using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichardBullet : MonoBehaviour
{
    public int damage;
    public float movementSpeed;
    public bool right;
    
    private float direction;

    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().flipX = !right;

        direction = right ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * direction * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            print("hit");
            hit.transform.GetComponent<BossController>().doDamage(damage);
            Destroy(gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
