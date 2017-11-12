using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneController : BossController {
    public Transform lanceSpawn;

    public float arrowOffset;
    
    public Transform birdSpawn;

    private Vector2 max;
    private Transform playerTrans;

	//public float bulletDamage;

    // Use this for initialization
    void Start () {
        baseStart();

        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        playerTrans = GameObject.Find("Player").transform;

        StartCoroutine(BossRoutine());
	}

    IEnumerator BossRoutine()
    {
        while (health > 70.0f)
        {
            phaseOne();
            yield return new WaitForSeconds(phaseTimes[0]);
        }

        while (health > 30.0f)
        {
            phaseTwo();
            yield return new WaitForSeconds(phaseTimes[1]);
        }

        while (health > 0.0f)
        {
            phaseThree();
            yield return new WaitForSeconds(phaseTimes[2]);
        }
    }

    void phaseOne()
    {
        Instantiate(attacks[0], lanceSpawn.position, lanceSpawn.rotation);
    }

    void phaseTwo()
    {
        Instantiate(attacks[1], new Vector3(playerTrans.position.x, max.y + arrowOffset, 0), new Quaternion(0, 0, 0, 0));
    }

    void phaseThree()
    {
        Instantiate(attacks[2], birdSpawn.position, birdSpawn.rotation);
    }
}
