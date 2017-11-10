using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	private Transform player;
	private Rigidbody2D rb2d;
	public float moveSpeed = 7;
	public float jumpStrength = 2;
	public GameObject leftBullet, rightBullet;
	public Transform firePos;
	public float jumpLimit = 7;
	private bool facingRight = true;
	// Use this for initialization
	void Start () {
		player = GetComponent<Transform> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			player.transform.position += new Vector3 (1, 0, 0) * moveSpeed * Time.deltaTime;
			ChangeFirePos(1);
			facingRight = true;
		}

		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			player.transform.position += new Vector3 (-1, 0, 0) * moveSpeed * Time.deltaTime;
			ChangeFirePos(-1);
			facingRight = false;
		}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			//player.transform.position += transform.up * jumpStrength * Time.deltaTime;
			rb2d.AddForce(new Vector2(0, 1) * jumpStrength * 100);
		}

		if (Input.GetKeyDown (KeyCode.Z)) 
		{
			Fire ();
		}
	}

	void Fire()
	{
		if (facingRight) {
			Instantiate (rightBullet, firePos.position, firePos.rotation);
		} else {
			Instantiate (leftBullet, firePos.position, firePos.rotation);
		}
	}

	void ChangeFirePos(int orientation)
	{
		firePos.position = new Vector2 (player.position.x + orientation, firePos.position.y);
	}
}
