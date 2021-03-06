﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BossController : MonoBehaviour
{
    public float health;
    public List<GameObject> attacks;
    public List<float> phaseTimes;

    public float damage;

    private GameObject player;

    public void baseStart()
    {
        player = GameObject.Find("Player");
    }

    public void doDamage(float playerDamage)
    {
        health -= playerDamage;
    }

    public void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerControls>().doDamage(damage);
        }
    }
}
