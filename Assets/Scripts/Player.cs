using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;

    [SerializeField] Transform groundChecker;
    [SerializeField] float groundCheckerRadius;
    [SerializeField] LayerMask groundLayers;

    bool isGrounded;

    float direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GroundCheck();
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
            speed *= -1;
        }

        rb.AddTorque(1.5f);

        currentVelocity.x = speed;
        rb.velocity = currentVelocity;
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckerRadius, groundLayers);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, groundCheckerRadius);
    }

}
