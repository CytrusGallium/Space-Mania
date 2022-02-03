using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingMissle : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 200f;

    public GameObject explosionEffect;

    private Transform target;
    private Rigidbody2D rb;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        if (go == null)
        {
            Explode();
            return;
        }
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            Explode();
            return;
        }
        
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
