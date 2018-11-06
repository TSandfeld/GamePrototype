using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 5f;

    [SerializeField]
    Transform groundCheck;

    protected bool jump = false;
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

    public void GetInput()
    {
        //float h = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && (grounded || jumpCount < 2))
        {
            grounded = false;
                
            rb.AddForce(new Vector2(0f, 400f));
            
            jump = true;
            jumpCount++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * 5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * 5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlatformTile")) {
            grounded = true;
            jumpCount = 0;
        }
    }
}
