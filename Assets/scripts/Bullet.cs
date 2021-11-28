using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float Bspeed;
    public Rigidbody2D rb;
    private CapsuleCollider2D capsule;
    Animator myAnimator;
    [SerializeField] AudioClip sfx_bullet;


    private void Start()
    { 
        
    }
    
    void Update()
    {
        //AudioSource.PlayClipAtPoint(sfx_bullet, Camera.main.transform.position);
        //transform.Translate(Vector3.right * Bspeed * Time.deltaTime); //velocidad de la bala   
    }

    public void Shoot(bool dir, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        if (dir)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        AudioSource.PlayClipAtPoint(sfx_bullet, Camera.main.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator = GetComponent<Animator>();
        rb.velocity = Vector2.zero;
        myAnimator.SetTrigger("Collision");
        var enemy = collision.GetComponent<ENemy1>();
        if (enemy)
        {
            enemy.TakeHit(1);
        }
        var Enemy2 = collision.GetComponent<Enemy>();
        if (Enemy2)
        {
            Enemy2.TakeHit(1);
        }
        var Enemy3= collision.GetComponent<Enemy3>();
        if (Enemy3)
        {
            Enemy3.TakeHit(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    void Die()
    {
        Destroy(gameObject);
    }
        /* private void OnCollisionEnter2D(Collision2D collision)
         {

             if (collision.gameObject.layer == 3) // si choca con la capa del suelo se hace la animacion y luego se comienza el ienumerator.
             {
                 transform.GetComponent<Animator>().SetBool("BulletOff", true);
                 StartCoroutine(DestroyBullet());

             }
         }

         IEnumerator DestroyBullet() //se usa para ejecutar en su totalidad la animacion de explosion.
         {
             Bspeed = 0;
             rb.velocity = new Vector2(0, rb.velocity.y);
             yield return new WaitForSeconds(0.2f);
             Destroy(gameObject);
         }

        /* private void OnTriggerEnter2D(Collider2D collision)
         {
             myAnimator.SetTrigger("Collision");
             rb.velocity = Vector2.zero;

         }

         public void shut (bool dir, float speed)
         {

         }

         void Die()
         {
             Destroy(gameObject);
         }*/

        
}
