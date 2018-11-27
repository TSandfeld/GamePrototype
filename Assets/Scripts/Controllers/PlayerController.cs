using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : PlayerAbstract {

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    float speed = 5f;

    public Text displayScore;

    [SerializeField]
    Stats health;

    protected Vector2 direction;

    [SerializeField]
    float BulletSpeed = 5f;

    Vector2 currentDirection = Vector2.up;

	// Use this for initialization
    protected override void Start () 
    {
        health.Initialize(base.GetHP(), base.initHP);
	}
	
	// Update is called once per frame
    void Update() 
    {
        displayScore.text = base.GetScoreText();
        GetInput();

        Move();
	}

    public void GetInput() 
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            PerformRangeAttack();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (base.isNPCPresent)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(base.NPCDialogue);
            }
        }

        if (!direction.Equals(Vector2.zero)) {
            currentDirection = direction;
        }
    }

    void PerformRangeAttack() {
        Bullet.transform.position = transform.position;

        var b = Instantiate(Bullet, Bullet.transform.position, Quaternion.identity);

        b.GetComponent<Rigidbody2D>().velocity = currentDirection * BulletSpeed;

        Destroy(b, 2.0f);
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void CollectItems()
    {
        base.CollectItem();
    }

    IEnumerator CollectTreasure()
    {
        speed = speed * 2;
        yield return new WaitForSeconds(5.0f);
        speed = speed / 2;
    }

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        health.MyCurrentValue = base.GetHP();

        /*if (HP <= 0)
        {
            //PLAYER DEAD!!! TODO: HANDLE
            Destroy(gameObject);
        }*/
    }

    protected void SetNPCPresence(bool condition)
    {
        base.isNPCPresent = condition;
    }

    protected void SetNPCDialogue(Dialogue dialogue)
    {
        base.NPCDialogue = dialogue;
    }
}
