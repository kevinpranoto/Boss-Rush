using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneController : BossController {
    public Transform lanceSpawn;

    public float arrowVerticalOffset;
    public float phaseTwoPartTwoTime;
    public float setArrowHorizontalOffset;
    
    public Transform birdSpawn;
    public int numberOfBirds;

    private Vector2 max;
    private Transform playerTrans;
    private float arrowHorizontalOffset;

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
            
            phaseTwoPartOne();
            yield return new WaitForSeconds(phaseTwoPartTwoTime);
            phaseTwoPartTwo();
            yield return new WaitForSeconds(phaseTimes[1]);
        }

        while (health > 0.0f)
        {
            if (playerTrans.childCount <= numberOfBirds)
            {
                phaseThree();
            }
            yield return new WaitForSeconds(Random.Range(0.25f, phaseTimes[2]));
        }
    }

    void phaseOne()
    {
        Instantiate(attacks[0], lanceSpawn.position, lanceSpawn.rotation);
    }

    void phaseTwoPartOne()
    {
        Instantiate(attacks[1], new Vector3(playerTrans.position.x, max.y + arrowVerticalOffset, 0), new Quaternion(0, 0, 0, 0));
    }

    void phaseTwoPartTwo()
    {
        arrowHorizontalOffset = Random.value >= 0.5f ? setArrowHorizontalOffset : -setArrowHorizontalOffset;

        Instantiate(attacks[1], new Vector3(playerTrans.position.x + arrowHorizontalOffset, max.y + arrowVerticalOffset, 0), new Quaternion(0, 0, 0, 0));
    }

    void phaseThree()
    {
        Instantiate(attacks[2], birdSpawn.position, birdSpawn.rotation);
    }
}
