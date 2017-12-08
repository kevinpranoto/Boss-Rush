using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy {
    public float hp;
    public float radius;
    public float rotationSpeed;
    public float trackedMovementSpeed = 1f;

    private bool tracked;
    private BoxCollider2D boxCollider;
	private PolygonCollider2D polyCollider;

	// Use this for initialization
	void Start () {
        baseStart();
        tracked = false;
        boxCollider = GetComponent<BoxCollider2D>();
		polyCollider = GetComponent<PolygonCollider2D> ();
        boxCollider.enabled = false;
		polyCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        baseUpdate();        

        if (Vector2.Distance(transform.position, player.transform.position) < radius)
        {
            
            if (!tracked)
            {
                movementSpeed = trackedMovementSpeed;
                polyCollider.enabled = true;
                transform.parent = player.transform;
                tracked = true;
            }

            transform.RotateAround(transform.parent.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.identity;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    public void doDamage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void OnBecameInvisible()
    {
        return;
    }

	public override void OnCollisionEnter2D(Collision2D hit)
	{
		if (hit.gameObject.CompareTag("Player"))
		{
			player.GetComponent<PlayerControls>().doDamage(damage);
			Destroy(gameObject);
		}
	}  

}
