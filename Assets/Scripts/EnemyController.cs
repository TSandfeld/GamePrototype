using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject player;

    [SerializeField]
    float speed = 1f;

    protected Vector2 direction;

    private bool playerDidEnter = false;
    private int HP = 3;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
    void Update () {
        GetDirection();

        Move();
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
}
