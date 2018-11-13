using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour {

    [SerializeField]
    GameObject bullet;

    protected Vector2 currentDirection;

    [SerializeField]
    float bulletSpeed = 5f;

    float timeLeft = 2.0f;
    float shootInterval = 2.0f;

	// Use this for initialization
	void Start () {
        currentDirection = new Vector2(-1, 0);
	}

    // Update is called once per frame
    void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            PerformRangeAttack();
            timeLeft = shootInterval;
        }
	}

    void PerformRangeAttack()
    {
        bullet.transform.position = transform.position;

        var b = Instantiate(bullet, bullet.transform.position, Quaternion.identity);

        b.GetComponent<Rigidbody2D>().velocity = currentDirection * bulletSpeed;

        Destroy(b, 1.0f);
    }
}
