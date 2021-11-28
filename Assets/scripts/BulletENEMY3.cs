using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletENEMY3 : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void shoot(bool dir, float speed)
    {
        rb = GetComponent<Rigidbody2D>();

        if (dir)
        {
            rb.velocity = new Vector2(1, 1);
            
        }
        else
        {
            rb.velocity = new Vector2(-1, 1);
           
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ally = collision.collider.GetComponent<LiifeMegaman>();
        if (ally)
        {
            ally.TakeDMG(1);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

