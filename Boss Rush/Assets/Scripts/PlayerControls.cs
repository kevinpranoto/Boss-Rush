using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    public float health;
	public float moveSpeed = 7;
	public float jumpStrength = 2;
    public float setJumpTimer = 3f;
    public float fallSpeed = 0.1f;
    public float setFlyTimer = 3f;
    public float setInvincibileTimer = 1f;
	public float setFireTimer = 0.2f;
	public float floatingNum = 4f;

    public LayerMask groundLayer;
    public LayerMask bossLayer;
    
    public GameObject bullet;

	private bool facingRight = true;
    private bool aimingUp = false;
	private bool aimingDown = false;
    private bool grounded;
    private float jumpTimer;
    private bool fly = false;
    private float flyTimer;
    private bool invicible = false;
    private float invincibleTimer;
	private float fireTimer;
	private bool fire = true;
    private bool jumping = false;

    private SpriteRenderer sprite;
	private Animator anim;

    private Vector2 min;
    private Vector2 max;
    private Vector3 move;

    private RaycastHit2D downRay;
    private RaycastHit2D upRay;
    private RaycastHit2D horizontalRay;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		flyTimer = setFlyTimer;
		fireTimer = setFireTimer;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0.04f, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(0.96f, 1));
    }

    void FixedUpdate()
    {
        if (!grounded && !fly && !jumping)
        {
            transform.Translate(new Vector2(0, -fallSpeed) * Time.deltaTime);
        }
        else if (!grounded && fly && !jumping)
        {
			
            //rb2d.velocity = rb2d.velocity - new Vector2 (0, fallSpeed / floatingNum);
        }
    }

    // Update is called once per frame
    void Update () 
	{
		print(transform.position.x);
		if (Time.timeScale != 0)
		{
			// Movement
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				sprite.flipX = false;
				facingRight = true;

				move = new Vector3(1, 0, 0) * moveSpeed * Time.fixedDeltaTime;

				if (transform.position.x + move.x <= max.x && transform.position.x + move.x >= min.x && !checkBossCollision(1))
				{
					transform.Translate(move);
				}
			}

			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				sprite.flipX = true;
				facingRight = false;

				move = new Vector3 (-1, 0, 0) * moveSpeed * Time.fixedDeltaTime;

				if (transform.position.x + move.x <= max.x && transform.position.x + move.x >= min.x && !checkBossCollision(-1))
				{
					transform.Translate (move);
				}
			}

			if (Input.GetKey(KeyCode.Space) && grounded && !jumping)
			{
                jumping = true;
                jumpTimer = setJumpTimer;
			}

            if (jumping && jumpTimer > 0)
            {
                jumpTimer -= Time.deltaTime;
                transform.Translate(new Vector2(0, jumpStrength) * Time.fixedDeltaTime);
            }
            else
            {
                jumping = false;
            }

            if (Input.GetKey(KeyCode.Space) && !grounded && !jumping && flyTimer > 0)
            {
				fly = true;
			}
			else
			{
				fly = false;
			}

			if (fly)
			{
				flyTimer -= Time.deltaTime;
				anim.SetInteger ("State", 1);
			}
			else
			{
				anim.SetInteger ("State", 0);
			}

			if (!fire)
			{
				fireTimer -= Time.deltaTime;
			}

			if (!fire && fireTimer <= 0)
			{
				fire = true;
				fireTimer = setFireTimer;
			}
			
			// Firing
			if (Input.GetKey (KeyCode.Z) && fire)
			{
				Fire ();
				fire = false;
			}

			isInvincible ();
		}
    }

    void Fire()
	{
        Vector3 offset = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            aimingUp = true;
            offset = new Vector3(0f, 1.5f, 0f);
        }

		else if (!grounded && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
		{
            aimingUp = false;
			aimingDown = true;
			offset = new Vector3(0f, -1.1f, 0f);
		}

        else
        {
            aimingUp = false;
			aimingDown = false;
            offset = facingRight ? new Vector3(1.5f, 0f, 0f) : new Vector3(-1.5f, 0f, 0f);
        }


        GameObject newBullet = Instantiate(bullet, transform.position + offset, Quaternion.identity);
        newBullet.GetComponent<RichardBullet>().up = aimingUp;
		newBullet.GetComponent<RichardBullet>().down = aimingDown;
        newBullet.GetComponent<RichardBullet>().right = facingRight;
    }

    void isInvincible()
    {
        if (invicible)
        {
            sprite.enabled = !sprite.enabled;
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer <= 0)
            {
                sprite.enabled = true;
                invicible = false;
            }
        }
    }

    bool checkBossCollision(float direction)
    {
        horizontalRay = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.8f, bossLayer);

        if (horizontalRay.collider != null)
        {
            return true;
        }

        return false;
    }

    public void doDamage(float dmg)
    {
        if (!invicible)
        {
            invincibleTimer = setInvincibileTimer;
            invicible = true;
            health -= dmg;

            if (health <= 0)
            {
                sprite.enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponent<PlayerControls>().enabled = false;
            }
        }
    }

    public bool isPlayerDead()
    {
        return (health <= 0);
    }

    public bool isPlayerInvincible()
    {
        return invicible;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
			flyTimer = setFlyTimer;
        }
    }
}
