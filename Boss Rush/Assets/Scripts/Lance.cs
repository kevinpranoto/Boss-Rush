using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : Enemy {

	// Use this for initialization
	void Start () {
        baseStart();
	}

    void Update()
    {
        baseUpdate();
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
}
