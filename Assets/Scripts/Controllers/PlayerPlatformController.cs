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

    Animator animator;

    protected bool jumping = false;
    protected bool grounded = true;

    protected int jumpCount = 0;

    bool collisionIsAbove;
    bool collisionIsEqualLeft;
    bool collisionIsEqualRight;
    bool collisionIsBelow;

    protected Vector2 direction;

    protected Vector2 startPosition;

    private Rigidbody2D rb;

    // Use this for initialization
    protected override void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        health.Initialize(base.GetHP(), base.initHP);
        animator = GetComponent<Animator>();

        startPosition = transform.position;
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

        if (Input.GetKey(KeyCode.A) && !collisionIsEqualRight)// && !collisionIsBelow)
        {
            Vector3 rVel = rb.velocity;
            rVel.x = -maxSpeed;
            rb.velocity = rVel;

            direction = Vector2.left;
            Move();
        }

        if (Input.GetKey(KeyCode.D) && !collisionIsEqualLeft)// && !collisionIsBelow)
        {
            Vector3 rVel = rb.velocity;
            rVel.x = maxSpeed;
            rb.velocity = rVel;

            direction = Vector2.right;
            Move();
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
			direction = Vector2.zero;
            Move();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (base.isNPCPresent)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(base.NPCDialogue);
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

        if (HP <= 0)
        {
            //PLAYER DEAD!
            ResetPlayerAndHealth();
        }
    }

    protected void ResetPlayerAndHealth() 
    {
        health.Initialize(100, base.initHP);
        base.ResetHP();
        transform.position = startPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetCollisionBools(collision);
        if (collision.collider.CompareTag("PlatformTile"))// && collisionIsAbove) 
        {

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

        print("otherX = " + otherX);
        print("otherY = " + otherY);
        print("contactX = " + contactX);
        print("contactY = " + contactY);


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
            //print("Right");
            //collisionIsAbove = false;
            //collisionIsEqualRight = true;
            //collisionIsEqualLeft = false;
            //collisionIsBelow = false;
        }
        else if (otherY.Equals(contactY) && otherX < contactX)
        {
            //print("Left");
            //collisionIsAbove = false;
            //collisionIsEqualRight = false;
            //collisionIsEqualLeft = true;
            //collisionIsBelow = false;
        }
        else if (otherY < contactY)
        {
            //print("Below");
            //collisionIsAbove = false;
            //collisionIsEqualLeft = false;
            //collisionIsEqualRight = false;
            //collisionIsBelow = true;
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

    protected void SetNPCPresence(bool condition)
    {
        base.isNPCPresent = condition;
    }

    protected void SetNPCDialogue(Dialogue dialogue)
    {
        base.NPCDialogue = dialogue;
    }

    protected void GetCurrentPlayerScoreLvl2(GameObject changeScene) 
    {
        if (base.GetPlayerScore().Equals(base.GetLvl2RequiredRecipes()))
        {
			changeScene.SendMessage("SetLvl2Done");
        }
    }

    protected void GetCurrentPlayerScoreLvl3(GameObject changeScene)
    {
        print(base.GetPlayerScore() + " player score");
        print(base.GetLvl3RequiredRecipes() + " req");
        if (base.GetPlayerScore().Equals(base.GetLvl3RequiredRecipes()))
        {
            changeScene.SendMessage("SetLvl3Done");
        }
    }

    protected void Move()
    {
        if (!direction.x.Equals(0f))
        {
            AnimateMovement();
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

    }

    protected void AnimateMovement() 
    {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
    }
}
