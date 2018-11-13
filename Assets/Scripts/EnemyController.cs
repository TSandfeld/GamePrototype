using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject player;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    float speed = 1f;

    protected Vector2 direction;

    [SerializeField]
    float BulletSpeed = 4f;

    [SerializeField]
    float MaxDistance = 5f;

    private bool playerDidEnter = false;
    private int HP = 3;

    float timer = 0.0f;

    [SerializeField]
    int waitingTime = 2;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
    void Update () {
        GetDirection();

        Move();

        PerformRangeAttack();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerDidEnter = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerDidEnter = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDidEnter = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDidEnter = false;
        }
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void GetDirection () {
        if (playerDidEnter) {
            direction = player.transform.position - transform.position;
        } else {
            direction = Vector2.zero;
        }
    }

    void TakeDamage(int damage) {
        HP -= damage;

        if (HP <= 0) {
            Destroy(gameObject);
        }
    }

    void PerformRangeAttack()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= MaxDistance)
            {
                Bullet.transform.position = transform.position;

                var b = Instantiate(Bullet, Bullet.transform.position, Quaternion.identity);

                b.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position) * BulletSpeed;

                Destroy(b, 2.0f);
                timer = 0;
            }

        }

    }

   
}
