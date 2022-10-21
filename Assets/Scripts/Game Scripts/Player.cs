using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public Action onEndGame;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;

    [SerializeField] float groundCheckerRadius;
    [SerializeField] LayerMask groundLayers;

    [SerializeField] float timeBetweenChangeDirection;

    [SerializeField] ParticleSystem deadParticles;

    Color transparentColor;

    float counterToChangeDirection = 0;

    bool inEndLevel;

    Vector3 spawnPosition;
    Vector2 currentVelocity;
    float direction;
    bool hasInversedGravity;
    bool isGrounded;
    bool justSpawned;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        spawnPosition = transform.position;
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
        if (!justSpawned && !inEndLevel)
        {
            Movement();
            GroundCheck();
            ManageCounters();
        }
        else if (!inEndLevel)
        {
            if (IsJumping())
            {
                justSpawned = false;
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
        currentVelocity.y = jumpForce;
    }

    void Respawn()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        transform.position = spawnPosition;
        justSpawned = true;
        rb.velocity = Vector3.zero;
        direction = 1;
        hasInversedGravity = false;
        inEndLevel = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void Die()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        deadParticles.Play();
        GetComponent<SpriteRenderer>().color = Color.clear;
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
            Debug.Log("End level");
            inEndLevel = true;
            onEndGame?.Invoke();

            GameManager.instance.UnlockLevel(collision.GetComponent<EndLevelCollider>().levelToUnlock);
        }
    }

    private void OnDrawGizmos()
    {
        //Vector2 offset = new Vector2(transform.position.x, transform.position.y + 0.18f);
        //Gizmos.DrawSphere(offset, groundCheckerRadius);
    }

}
