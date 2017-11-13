using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy {
    public float rotationSpeed;

    //private Vector3 pivot;
    private bool tracked;

	// Use this for initialization
	void Start () {
        baseStart();
        //pivot = player.transform.position - transform.position;
        tracked = false;
    }
	
	// Update is called once per frame
	void Update () {
        baseUpdate();        

        if (Vector2.Distance(transform.position, player.transform.position) < 5.0f)
        {
            
            if (!tracked)
            {
                movementSpeed = 1f;
                //pivot = transform.position - player.transform.position;
                transform.parent = player.transform;
                tracked = true;
            }
            /*
            pivot = (Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * pivot);

            transform.position = player.transform.position + pivot;*/

            transform.RotateAround(transform.parent.transform.position, Vector3.forward, rotationSpeed);
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
    }

}
