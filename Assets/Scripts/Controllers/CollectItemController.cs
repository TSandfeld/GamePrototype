using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectItemController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var Player = collision.gameObject;

            if (SceneManager.GetActiveScene().name.Equals("Level 1"))
            {
				Player.SendMessage("CollectItems", int.Parse(gameObject.name));
            } 
            else 
            {
                Player.SendMessage("CollectItems");    
            }

            Destroy(gameObject);
        }
    }
}
