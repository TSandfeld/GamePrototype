using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 5f;

    [SerializeField]
    Transform groundCheck;

    protected bool jumping = false;
    protected bool grounded = true;

    protected int jumpCount = 0;

    protected Vector2 direction;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        GetInput();
    }

    void LateUpdate()
    {
        
    }

    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && (grounded || jumpCount < 2))
        {
            grounded = false;

            //rb.AddForce(new Vector2(0f, 400f));
            Vector3 rVel = rb.velocity;
            rVel.y = maxSpeed * 1.2f;
            rb.velocity = rVel;

            jumping = true;
            jumpCount++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rVel = rb.velocity;
            rVel.x = -maxSpeed;
            rb.velocity = rVel;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 rVel = rb.velocity;
            rVel.x = maxSpeed;
            rb.velocity = rVel;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (!grounded) 
            {
				rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.1f);
            }
            else 
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlatformTile")) {
            grounded = true;

            if (jumping) 
            {
                rb.velocity = Vector2.zero;
            }

            jumpCount = 0;
            jumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlatformTile"))
        {
            grounded = false;
        }
    }
}
