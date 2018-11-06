using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.GetType().Equals(typeof(BoxCollider2D))) {
            var Enemy = collision.gameObject;

            Enemy.SendMessage("TakeDamage", 1);
            Destroy(gameObject);
        }
    }

}
