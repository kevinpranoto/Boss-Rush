using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy {
    public float hp;
    public float rotationSpeed;
    public float trackedMovementSpeed = 1f;

    //private Vector3 pivot;
    private bool tracked;
    private BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {
        baseStart();
        //pivot = player.transform.position - transform.position;
        tracked = false;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        baseUpdate();        

        if (Vector2.Distance(transform.position, player.transform.position) < 5.0f)
        {
            
            if (!tracked)
            {
                movementSpeed = trackedMovementSpeed;
                boxCollider.enabled = true;
                //pivot = transform.position - player.transform.position;
                transform.parent = player.transform;
                tracked = true;
            }
            /*
            pivot = (Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * pivot);

            transform.position = player.transform.position + pivot;*/

            transform.RotateAround(transform.parent.transform.position, Vector3.forward, rotationSpeed);
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

}
