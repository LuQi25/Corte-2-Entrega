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

    public float coolddown = 2; //cooldown para ritmo que salen las balas cada cierto tiempo
    private float nextTime;
    

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    
    void Update()
    {
        transform.Translate(Vector3.right * Bspeed * Time.deltaTime); //velocidad de la bala
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
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



}
