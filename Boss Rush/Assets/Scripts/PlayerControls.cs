using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    public float health;
	private Rigidbody2D rb2d;
	public float moveSpeed = 7;
	public float jumpStrength = 2;
    //public GameObject leftBullet, rightBullet;

    public GameObject bullet;

    public Transform firePos;
	public float jumpLimit = 7;
	private bool facingRight = true;
    private SpriteRenderer sprite;

    private Vector2 min;
    private Vector2 max;
    private Vector3 move;

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
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = false;
            ChangeFirePos(1);
            facingRight = true;

            move = new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;

            print(transform.position.x + move.x <= max.x && transform.position.x + move.x >= min.x);

            if (transform.position.x + move.x <= max.x && transform.position.x + move.x >= min.x)
            {
                transform.position += move;
            }
		}

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
            ChangeFirePos(-1);
            sprite.flipX = true;
            facingRight = false;

            move = new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;

            if (transform.position.x + move.x <= max.x && transform.position.x + move.x >= min.x)
            {
                transform.position += move;
            }
        }

		if (Input.GetKeyDown(KeyCode.Space))
		{
			//player.transform.position += transform.up * jumpStrength * Time.deltaTime;
			rb2d.AddForce(new Vector2(0, 1) * jumpStrength * 100);
		}

		if (Input.GetKeyDown(KeyCode.Z)) 
		{
			Fire();
		}
	}

	void Fire()
	{
        GameObject newBullet = Instantiate(bullet, firePos.position, firePos.rotation);
        newBullet.GetComponent<RichardBullet>().right = facingRight;

        /*
		if (facingRight) {
			Instantiate(rightBullet, firePos.position, firePos.rotation);
		} else {
			Instantiate(leftBullet, firePos.position, firePos.rotation);
		}*/
    }

    void ChangeFirePos(int orientation)
	{
		firePos.position = new Vector2(transform.position.x + orientation, firePos.position.y);
	}

    public void doDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            sprite.enabled = false;
            GetComponent<PlayerControls>().enabled = false;
        }
    }

    public bool isPlayerDead()
    {
        return (health <= 0);
    }
}
