using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour {

    [SerializeField]
    float speed = 3.5f;

    protected Vector2 direction;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
    protected virtual void Update() 
    {
        Move();
	}

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
