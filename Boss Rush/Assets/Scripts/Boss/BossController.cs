using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BossController : MonoBehaviour
{
    public float health;
    public List<GameObject> attacks;
    public List<float> phaseTimes;

    public int damage;

    private GameObject player;

    public void baseStart()
    {
        player = GameObject.Find("Player");
    }

    public void doDamage(float dmg)
    {
        health -= dmg;
    }

    /*public void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            print("hit");
            player.GetComponent<PlayerControls>().doDamage(damage);
        }
    }*/
}
