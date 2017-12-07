using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Enemy {
    public float initialStopTime;
    public float delayTime;

    private bool moving;

	private float rightBoundary = 4.8f;
	private float leftBoundary = -8.7f;
	private float shiftBy = 1.2f;

    // Use this for initialization
    void Start()
    {
        baseStart();
        StartCoroutine(ArrowWarning());
    }

    void Update()
    {
        baseUpdate();
        if (moving)
        {
            transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
			Vector3 p = transform.position;
			if (p.x > rightBoundary) 
			{
				p.x = rightBoundary - shiftBy;
				transform.position = p;
			}

			if (p.x < leftBoundary) 
			{
				p.x = leftBoundary + shiftBy;
				transform.position = p;
			}
        }
    }

    IEnumerator ArrowWarning()
    {
        moving = true;
        yield return new WaitForSeconds(initialStopTime);
        moving = false;
        yield return new WaitForSeconds(delayTime);
        moving = true;
    }
}
