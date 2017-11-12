using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BossController : MonoBehaviour
{
    public float health;
    public List<GameObject> attacks;
    public List<float> phaseTimes;

    public void baseStart()
    {

    }

    public void doDamage(float dmg)
    {
        health -= dmg;
    }
}
