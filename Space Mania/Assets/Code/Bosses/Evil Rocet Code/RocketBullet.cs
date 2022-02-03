using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet: MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealthAndShield health = hitInfo.GetComponent<PlayerHealthAndShield>();

        if (health != null)
        {
            health.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
