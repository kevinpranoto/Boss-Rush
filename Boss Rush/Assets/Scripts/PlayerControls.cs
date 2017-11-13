﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    public float health;
	public float moveSpeed = 7;
	public float jumpStrength = 2;
    //public float jumpLimit = 7;
    public float fallSpeed = 0.1f;
    public LayerMask groundLayer;

    //public GameObject leftBullet, rightBullet;
    public GameObject bullet;

    //public Transform firePos;

	private bool facingRight = true;
    private bool grounded;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;

    private Vector2 min;
    private Vector2 max;
    private Vector3 move;

    private RaycastHit2D downRay;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        min = Camera.main.ViewportToWorldPoint(new Vector2(0.04f, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(0.96f, 1));
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Z)) 
		{
			Fire();
		}
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = false;
            //ChangeFirePos(1.5f);
            facingRight = true;

            move = new Vector3(1, 0, 0) * moveSpeed * Time.fixedDeltaTime;

            if (transform.position.x + move.x <= max.x && transform.position.x + move.x >= min.x)
            {
                //rb2d.MovePosition(rb2d.position + move);
                //transform.position += move;
                transform.Translate(move);
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            sprite.flipX = true;
            //ChangeFirePos(-1.5f);
            facingRight = false;

            move = new Vector3(-1, 0, 0) * moveSpeed * Time.fixedDeltaTime;

            if (transform.position.x + move.x <= max.x && transform.position.x + move.x >= min.x)
            {
                //rb2d.MovePosition(rb2d.position + move);
                //transform.position += move;
                transform.Translate(move);
            }
        }

        grounded = isGrounded();

        if (!grounded)
        {
            rb2d.velocity = rb2d.velocity - new Vector2(0, fallSpeed);
        }
        else
        {
            rb2d.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //player.transform.position += transform.up * jumpStrength * Time.deltaTime;
            //rb2d.AddForce(new Vector2(0, 1) * jumpStrength * 100);
            rb2d.velocity = new Vector2(0, jumpStrength);
        }
    }

    void Fire()
	{
        Vector3 offset = facingRight ? new Vector3(1.5f, 0f, 0f) : new Vector3(-1.5f, 0f, 0f);

        GameObject newBullet = Instantiate(bullet, transform.position + offset, Quaternion.identity);
        newBullet.GetComponent<RichardBullet>().right = facingRight;

        /*
		if (facingRight) {
			Instantiate(rightBullet, firePos.position, firePos.rotation);
		} else {
			Instantiate(leftBullet, firePos.position, firePos.rotation);
		}*/
    }

    /*void ChangeFirePos(float offset)
	{
        firePos = new Vector3(transform.position.x + offset, transform.position.y, 0);
		//firePos.position = new Vector2(transform.position.x + offset, firePos.position.y);
	}*/

    public void doDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            sprite.enabled = false;
            GetComponent<PlayerControls>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public bool isPlayerDead()
    {
        return (health <= 0);
    }

    bool isGrounded()
    {
        downRay = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer);

        if (downRay.collider != null)
        {
            return true;
        }

        return false;
    }
}
