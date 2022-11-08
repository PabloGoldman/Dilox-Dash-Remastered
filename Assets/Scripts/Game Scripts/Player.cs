using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public Action onEndGame;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;

    AudioSource source;
    [SerializeField] AudioClip deathSound;

    [SerializeField] float groundCheckerRadius;
    [SerializeField] LayerMask groundLayers;

    [SerializeField] float timeBetweenChangeDirection;

    [SerializeField] ParticleSystem deadParticles;
    [SerializeField] ParticleSystem runParticles;

    Color transparentColor;

    float counterToChangeDirection = 0;

    bool inEndLevel;

    float initialGravityScale = 6;

    Vector3 spawnPosition;
    Quaternion initialRotation;

    Vector2 currentVelocity;
    float direction;
    bool hasInversedGravity;
    bool isGrounded;
    bool justSpawned;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        source = GetComponent<AudioSource>();

        spawnPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.GetPlayerAvatar();

        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inEndLevel)
        {
            if (!justSpawned)
            {
                Movement();
                GroundCheck();
                ManageCounters();
            }
            else
            {
                if (IsJumping())
                {
                    justSpawned = false;
                    runParticles.gameObject.SetActive(true);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddTorque(1.5f);
    }

    void Movement()
    {
        currentVelocity = rb.velocity;

        if (IsJumping() && isGrounded)
        {
            Jump();
        }

        if (IsChangingDirection() && AbleToChangeDirection())
        {
            ChangeDirection();
        }

        currentVelocity.x = speed * direction;
        rb.velocity = currentVelocity;
    }

    bool IsChangingDirection()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.position.x < Screen.width / 2 && speed != 0f && Time.timeScale > 0)
            {
                return true;
            }
        }

        return Input.GetButtonDown("Vertical");
    }

    bool IsJumping()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.position.x > Screen.width / 2)
            {
                return true;
            }
        }

        return Input.GetButton("Jump");
    }

    bool AbleToChangeDirection()
    {
        return counterToChangeDirection <= 0;
    }

    void ChangeDirection()
    {
        direction *= -1;
        counterToChangeDirection = timeBetweenChangeDirection;
    }

    void ManageCounters()
    {
        if (counterToChangeDirection > 0)
        {
            counterToChangeDirection -= Time.deltaTime;
        }
    }

    void Jump()
    {
        if (!hasInversedGravity)
        {
            currentVelocity.y = jumpForce;
        }
        else
        {
            currentVelocity.y = -jumpForce;
        }
    }

    void Respawn()
    {
        rb.constraints = RigidbodyConstraints2D.None;

        transform.position = spawnPosition;

        transform.rotation = initialRotation;

        justSpawned = true;

        rb.velocity = Vector3.zero;

        rb.gravityScale = initialGravityScale;

        direction = 1;

        hasInversedGravity = false;

        inEndLevel = false;

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void Die()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        source.PlayOneShot(deathSound);
        deadParticles.Play();

        runParticles.gameObject.SetActive(false);

        GetComponent<SpriteRenderer>().color = Color.clear;

        AdsManager.instance.ShowDeathAd();

        Invoke(nameof(Respawn), 1f);
    }

    private void GroundCheck()
    {
        if (!hasInversedGravity)
        {
            Vector2 offset = new Vector2(transform.position.x, transform.position.y - 0.35f);

            isGrounded = Physics2D.OverlapCircle(offset, groundCheckerRadius, groundLayers);
        }
        else
        {
            Vector2 offset = new Vector2(transform.position.x, transform.position.y + 0.18f);

            isGrounded = Physics2D.OverlapCircle(offset, groundCheckerRadius, groundLayers);
        }
    }

    void WinLevel()
    {
        Debug.Log("End level");
        inEndLevel = true;
        onEndGame?.Invoke();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //AdsManager.instance.ShowAdWithDelay();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("BouncyIce"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * collision.gameObject.GetComponent<BouncyIce>().amountOfBounciness, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndLevel"))
        {
            WinLevel();

            GameManager.instance.UnlockLevel(collision.GetComponent<EndLevelCollider>().levelToUnlock);
        }

        if (collision.gameObject.CompareTag("Reverse Gravity"))
        {
            rb.gravityScale *= -1;
            hasInversedGravity = !hasInversedGravity;
        }
    }

    private void OnDrawGizmos()
    {
        //Vector2 offset = new Vector2(transform.position.x, transform.position.y + 0.18f);
        //Gizmos.DrawSphere(offset, groundCheckerRadius);
    }

}
