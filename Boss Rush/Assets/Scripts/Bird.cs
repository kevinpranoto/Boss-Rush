using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy {
    public float rotationSpeed;

    private Vector3 pivot;
    private bool tracked;
    private float radius = 5.0f;

	// Use this for initialization
	void Start () {
        baseStart();
        //pivot = player.transform.position - transform.position;
        tracked = false;
    }
	
	// Update is called once per frame
	void Update () {
        baseUpdate();

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.transform.position) < 5.0f)
        {
            if (!tracked)
            {
                pivot = transform.position - player.transform.position;
                tracked = true;
            }
            pivot = (Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.forward) * pivot);

            transform.position = player.transform.position + pivot;
        }   
    }

}
