using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    float speed = 5f;

    protected Vector2 direction;

    [SerializeField]
    float BulletSpeed = 5f;

    Vector2 currentDirection = Vector2.up;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
    void Update() 
    {
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
        if (Input.GetKeyDown(KeyCode.Space)) {
            PerformRangeAttack();
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
}
