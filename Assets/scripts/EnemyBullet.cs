using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float DieTime, damage;
    public Rigidbody2D rb;
    Animator myAnimator;
    
    
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
        
    }

    
    public void shoot(bool dir, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (dir)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }
    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(DieTime);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*var ally = collision.collider.GetComponent<LiifeMegaman>();
        if (ally)
        {
            ally.TakeDMG(1);
        }*/

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            myAnimator.SetBool("IsFly", false);
            Destroy(gameObject);
        }
        else
        {
            myAnimator.SetBool("IsFly", true);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
