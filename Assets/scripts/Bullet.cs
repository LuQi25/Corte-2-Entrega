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
    }


    
    void Update()
    {
        transform.Translate(Vector3.right * Bspeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Bspeed = 0;
        Destroy(gameObject);


        if (collision.gameObject.layer == 3)
        {
            //nextTime = Time.time + coolddown;
           // myAnimator.SetBool("BulletOff", true);
            //if (Time.time > nextTime)
            //{

                //Destroy(gameObject);

            //}
        }
        else
        {
            //myAnimator.SetBool("BulletOff", false);
        }
        

    }



}
