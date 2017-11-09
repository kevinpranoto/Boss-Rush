using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	private Transform player;
	private Rigidbody2D rb2d;
	public float moveSpeed = 7;
	public float jumpStrength = 7;
	public GameObject leftBullet, rightBullet;
	public Transform firePos;
	private bool facingRight = true;
	// Use this for initialization
	void Start () {
		player = GetComponent<Transform> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			player.transform.position += new Vector3 (0.1f, 0, 0);
			ChangeFirePos(1);
			facingRight = true;
		}

		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			player.transform.position += new Vector3 (-0.1f, 0, 0);
			ChangeFirePos(-1);
			facingRight = false;
		}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			rb2d.AddForce (new Vector3 (0, 1, 0) * jumpStrength);
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
		firePos.position = new Vector2 (rb2d.position.x + orientation, firePos.position.y);
	}
}
