using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObjects 
{
    public float health = 3;
	public float jumpStrength = 7;
	public float maxSpeed = 7;
	public float hoverStrength = 7;
	public GameObject leftBullet, rightBullet;
	public Transform firePos;
	private bool facingRight = true;
	/*
	public float speed;
    public float jumpHeight;
    private Vector3 movement;
    */

	void Start () 
	{
		
	}

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.RightArrow)) {
			firePos.position = new Vector2 (rb2d.position.x + 1, firePos.position.y);
			facingRight = true;
		} else if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.LeftArrow)) {
			firePos.position = new Vector2 (rb2d.position.x - 1, firePos.position.y);
			facingRight = false;
		}

		if (Input.GetButtonDown ("Jump") && grounded) 
		{
			velocity.y = jumpStrength;
		} 
		else if (Input.GetButtonUp ("Jump")) 
		{
			if (velocity.y > 0) 
			{
				velocity.y = velocity.y * 0.5f;
			}
		}

  		targetVelocity = move * maxSpeed;

		if (Input.GetKeyDown (KeyCode.Z)) 
		{
			Fire ();
		}
	}

    public void doDamage(float dmg)
    {
        health -= dmg;
    }

	void Fire()
	{
		if (facingRight) {
			Instantiate (rightBullet, firePos.position, firePos.rotation);
		} else {
			Instantiate (leftBullet, firePos.position, firePos.rotation);
		}
	}
	/*
	void Update () 
	{
		movement = new Vector3 (Input.GetAxis("Horizontal"), 0, 0);

		transform.position += movement * speed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
        {
			transform.position += new Vector3 (0, jumpHeight, 0) * speed * Time.deltaTime;
        }
	}*/
}