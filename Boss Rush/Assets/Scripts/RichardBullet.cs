using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichardBullet : MonoBehaviour
{
    public int damage;
    public float movementSpeed;
    public bool up = false;
    public bool right = true;
    
    private float direction = 1f;

    // Use this for initialization
    void Start()
    {
        if (up)
        {
            transform.rotation = new Quaternion(0f, 0f, 1f, 1f);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = !right;

            direction = right ? 1f : -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * direction * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.CompareTag("Bird"))
        {
            hit.transform.GetComponent<Bird>().doDamage(damage);
            Destroy(gameObject);
        }

        if (hit.gameObject.CompareTag("Enemy"))
        {
            hit.transform.GetComponent<BossController>().doDamage(damage);
            Destroy(gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
