using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatformController : PlayerAbstract
{
    // UI
    public Text displayScore;

    [SerializeField]
    Stats health;
    // UI

    [SerializeField]
    float maxSpeed = 4f;
    float jumpPower = 5f;

    [SerializeField]
    Transform groundCheck;

    protected bool jumping = false;
    protected bool grounded = true;

    protected int jumpCount = 0;

    bool collisionIsAbove;
    bool collisionIsEqualLeft;
    bool collisionIsEqualRight;
    bool collisionIsBelow;

    protected Vector2 direction;

    private Rigidbody2D rb;

    // Use this for initialization
    protected override void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        health.Initialize(base.GetHP(), base.initHP);
    }

    // Update is called once per frame
    void Update()
    {
        displayScore.text = base.GetScoreText();
        GetInput();
    }

    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && (grounded || jumpCount < 2))
        {
            grounded = false;

            Vector3 rVel = rb.velocity;
            rVel.y = jumpPower * 1.3f;
            rb.velocity = rVel;

            jumping = true;
            jumpCount++;
        }
        if (Input.GetKey(KeyCode.A) && !collisionIsEqualRight && !collisionIsBelow)
        {
            Vector3 rVel = rb.velocity;
            rVel.x = -maxSpeed;
            rb.velocity = rVel;
        }
        if (Input.GetKey(KeyCode.D) && !collisionIsEqualLeft && !collisionIsBelow)
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

    void CollectItems()
    {
        base.CollectItem();
    }

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        health.MyCurrentValue = base.GetHP();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetCollisionBools(collision);
        if (collision.collider.CompareTag("PlatformTile") && collisionIsAbove) {

            if (!grounded) 
            {
                rb.velocity = Vector2.zero;
            }

            grounded = true;

            jumpCount = 0;
            jumping = false;
        }
    }

    void SetCollisionBools(Collision2D collision)
    {
        var otherX = float.Parse(collision.otherCollider.bounds.center.x.ToString("0.00"));
        var otherY = float.Parse(collision.otherCollider.bounds.center.y.ToString("0.00"));
		var contactX = float.Parse(collision.contacts[0].point.x.ToString("0.00"));
        var contactY = float.Parse(collision.contacts[0].point.y.ToString("0.00"));

        //print("");
        //print("OtherX: " + otherX);
        //print("ContactX: " + contactX);
        //print("OtherY: " + otherY);
        //print("ContactY: " + contactY);
        //print("");

        if (otherY > contactY)
        {
            print("Above");
            collisionIsAbove = true;
            collisionIsEqualLeft = false;
            collisionIsEqualRight = false;
            collisionIsBelow = false;
        }
        else if (otherY.Equals(contactY) && otherX > contactX)
        {
            print("Right");
            collisionIsAbove = false;
            collisionIsEqualRight = true;
            collisionIsEqualLeft = false;
            collisionIsBelow = false;
        }
        else if (otherY.Equals(contactY) && otherX < contactX)
        {
            print("Left");
            collisionIsAbove = false;
            collisionIsEqualRight = false;
            collisionIsEqualLeft = true;
            collisionIsBelow = false;
        }
        else if (otherY < contactY)
        {
            print("Below");
            collisionIsAbove = false;
            collisionIsEqualLeft = false;
            collisionIsEqualRight = false;
            collisionIsBelow = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlatformTile"))
        {
            grounded = false;

            collisionIsAbove = false;
            collisionIsEqualLeft = false;
            collisionIsEqualRight = false;
            collisionIsBelow = false;
        }
    }
}
