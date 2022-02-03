using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        PlayerHealthAndShield health = hitInfo.GetComponent<PlayerHealthAndShield>();
        BossHealth bossHealth = hitInfo.GetComponent<BossHealth>();

        if (enemy != null)
        {
            enemy.TakeDamege(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (health != null)
        {
            health.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (hitInfo.CompareTag("Ground"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }


    }
}
