using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;

    [SerializeField] float groundCheckerRadius;
    [SerializeField] LayerMask groundLayers;

    Sprite avatarSprite;

    bool hasInversedGravity;

    bool isGrounded;

    Vector3 spawnPosition;

    bool justSpawned;

    float direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        spawnPosition = transform.position;

        avatarSprite = GameManager.instance.GetPlayerAvatar();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = avatarSprite;

        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!justSpawned)
        {
            Movement();
            GroundCheck();
        }
        else
        {
            if (Input.GetButton("Jump"))
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
        Vector2 currentVelocity = rb.velocity;

        if (Input.GetButton("Jump") && isGrounded)
        {
            currentVelocity.y = jumpForce;
        }

        if (Input.GetButtonDown("Vertical"))
        {
            direction *= -1;
        }

        currentVelocity.x = speed * direction;
        rb.velocity = currentVelocity;
    }

    void Respawn()
    {
        transform.position = spawnPosition;
        justSpawned = true;
        rb.velocity = Vector3.zero;
        direction = 1;
        hasInversedGravity = false;
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
            Respawn();
        }
    }

    private void OnDrawGizmos()
    {
        //Vector2 offset = new Vector2(transform.position.x, transform.position.y + 0.18f);
        //Gizmos.DrawSphere(offset, groundCheckerRadius);
    }

}
