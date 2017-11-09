﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneController : MonoBehaviour {
    public float health;
    public List<GameObject> attacks;

    public float phaseOneTime;
    public Transform lanceSpawn;

    public float phaseTwoTime;
    public float arrowOffset;

    public float phaseThreeTime;
    public Transform birdSpawn;

    private Vector2 max;
    private Transform player;

	public float bulletDamage;

    // Use this for initialization
    void Start () {
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        player = GameObject.Find("Player").transform;

        StartCoroutine(BossRoutine());
	}

    IEnumerator BossRoutine()
    {
        while (health > 70.0f)
        {
            phaseOne();
            yield return new WaitForSeconds(phaseOneTime);
        }

        while (health > 30.0f)
        {
            phaseTwo();
            yield return new WaitForSeconds(phaseTwoTime);
        }

        while (health > 0.0f)
        {
            phaseThree();
            yield return new WaitForSeconds(phaseThreeTime);
        }
    }

    void phaseOne()
    {
        Instantiate(attacks[0], lanceSpawn.position, lanceSpawn.rotation);
    }

    void phaseTwo()
    {
        Instantiate(attacks[1], new Vector3(player.position.x, max.y + arrowOffset, 0), new Quaternion(0, 0, 0, 0));
    }

    void phaseThree()
    {
        Instantiate(attacks[2], birdSpawn.position, birdSpawn.rotation);
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name == "LeftBullet(Clone)" || col.gameObject.name == "RightBullet(Clone)") {
			health -= bulletDamage;
		}
	}
}
