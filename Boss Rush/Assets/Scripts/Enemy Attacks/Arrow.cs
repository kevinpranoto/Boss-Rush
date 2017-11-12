using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Enemy {
    public float initialStopTime;
    public float delayTime;

    private bool moving;

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
