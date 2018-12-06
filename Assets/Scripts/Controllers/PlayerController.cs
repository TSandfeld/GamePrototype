using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : PlayerAbstract {

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    float speed = 5f;

    private Animator animator;

    public Text displayScore;

    [SerializeField]
    Stats health;

    protected Vector2 direction;

    [SerializeField]
    float BulletSpeed = 8f;

    Vector2 currentDirection = Vector2.up;

	// Use this for initialization
    protected override void Start () 
    {
        health.Initialize(base.GetHP(), base.initHP);
        animator = GetComponent<Animator>();
        transform.position = base.GetCurSpawnPos();
	}

    private void Awake()
    {
        // Set Spawn Position
        transform.position = base.GetCurSpawnPos();

        // Map positions of scrolls
        if (base.GetPlayerScore() < 1)
        {
            print(base.pickUps);
            base.pickUps = GameObject.FindGameObjectWithTag("Pickups");
            for (var i = 0; i < base.GetLvl1RecipesCount(); i++)
            {
                var pickup = base.pickUps.transform.GetChild(i).gameObject;
                
                pickup.name = i.ToString();
                pickup.SetActive(true);
                base.GetCollected()[i] = false;
            }
        } else {
            print("Pickups are set. Reloading pos.");
            base.pickUps = GameObject.FindGameObjectWithTag("Pickups");
            for (var i = 0; i < base.GetLvl1RecipesCount(); i++)
            {
                var pickup = base.pickUps.transform.GetChild(i).gameObject;
                pickup.name = i.ToString();
                pickup.SetActive(!base.GetCollected()[i]);
            }
        }

        // Check entrances
        if (base.GetLvl3RequiredRecipes() > 0) 
        {
            print("lvl 3 completed - hiding entrance");

            var entrance = GameObject.FindGameObjectWithTag("Entrance - Lvl 3");
            entrance.GetComponent<BoxCollider2D>().enabled = false;

            var closedEntranceSprite = GameObject.FindGameObjectWithTag("Entrance - Closed");

            entrance.GetComponent<SpriteRenderer>().sprite = closedEntranceSprite.GetComponent<SpriteRenderer>().sprite;
        } 
        if (base.GetLvl2RequiredRecipes() > 0) 
        {
            print("lvl 2 completed - hiding entrance");

            var entrance = GameObject.FindGameObjectWithTag("Entrance - Lvl 2");
            entrance.GetComponent<BoxCollider2D>().enabled = false;

            var closedEntranceSprite = GameObject.FindGameObjectWithTag("Entrance - Closed");

            entrance.GetComponent<SpriteRenderer>().sprite = closedEntranceSprite.GetComponent<SpriteRenderer>().sprite;
        }
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

        if (!direction.x.Equals(0f) || !direction.y.Equals(0f))
        {
			AnimateMovement(direction);         
        } else 
        {
            animator.SetLayerWeight(1, 0);
        }

    }

    void CollectItems(int id)
    {
        print("Collected item with id: " + id);
        base.GetCollected()[id] = true;
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

        if (HP <= 0)
        {
            //PLAYER DEAD!!! TODO: HANDLE
            //Destroy(gameObject);
            base.ResetHP();
            base.SetCurSpawnPos(base.GetMapSpawnPos());
            new ChangeScene().SwitchScenes("Dead Scene");
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

    protected void SetRequiredLvl2Count() 
    {
        base.SetLvl2RequiredRecipes(base.GetPlayerScore() + base.GetLvl2RecipesCount());
        base.SetCurSpawnPos(base.GetLvl2ReturnPos());
    }

    protected void SetRequiredLvl3Count()
    {
        base.SetLvl3RequiredRecipes(base.GetPlayerScore() + base.GetLvl3RecipesCount());
        base.SetCurSpawnPos(base.GetLvl3ReturnPos());
    }

    protected void AnimateMovement(Vector2 direction) 
    {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
