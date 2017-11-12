using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour {
    public int damage;
    public float movementSpeed;

    public GameObject player;

    public void baseStart()
    {
        player = GameObject.Find("Player");
    }

    public void baseUpdate()
    {

    }

    public void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerControls>().doDamage(damage);
            Destroy(gameObject);
        }
    }   

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
