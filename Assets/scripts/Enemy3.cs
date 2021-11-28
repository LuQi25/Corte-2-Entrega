using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [SerializeField] AudioClip sfx_deadEn3;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject point1;
    public float speedB;
    public float TimeBTShoots;
    public float ShootTIme;
    public bool EnemyDetect = false;
    [SerializeField] Transform SHposition;
    [SerializeField] Transform SHposition2;
    float nextFireIn;
    public Animator myanimator;
    public bool CanShoot;
    public float Hitpoints;
    public float MaxHitPoints = 5; //vida max
    

    void Start()
    {
        myanimator = GetComponent<Animator>();
        Hitpoints = MaxHitPoints;
    }


    void Update()
    {

        detectarEnemy();
        animaciondisparar();

    }


    bool detectarEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 5f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.left * 5f, Color.red);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right, 5f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.right * 5f, Color.blue);

        if (hit)
        {
            
            if (hit.collider.gameObject.tag == "Player")
            {
                EnemyDetect = true;

                if (Time.time >= nextFireIn)
                {
                    CanShoot = true;
                    nextFireIn = Time.time + TimeBTShoots;
                    disparah();
                    
                }

            }
            else
            {
                EnemyDetect = false;
            }


        }
        



        if (hit2)
        {
            
            if (hit2.collider.gameObject.tag == "Player")
            {
                EnemyDetect = true;
                if (Time.time >= nextFireIn)
                {
                    CanShoot = true;
                    nextFireIn = Time.time + TimeBTShoots;
                    disparah();   
                }
                
            }
            else
            {
                EnemyDetect = false;
            }

        }

        return hit.collider != null;
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        if (Hitpoints <= 0)
        {
            AudioSource.PlayClipAtPoint(sfx_deadEn3, Camera.main.transform.position);
            myanimator.SetBool("ISded", true);
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    void disparah()
    {
        if (EnemyDetect == true)
        {
            GameObject bala = Instantiate(Bullet, SHposition.position, Quaternion.identity);
            GameObject bala2 = Instantiate(Bullet, SHposition2.position, Quaternion.identity);
            bool direccion = transform.localScale.x == -1;
            bool direccion2 = transform.localScale.x == 1;
            (bala.GetComponent<BulletENEMY3>()).shoot(direccion, speedB * 2);
            (bala2.GetComponent<BulletENEMY3>()).shoot(direccion2, speedB * 2);

        }
        
    }
    void animaciondisparar()
    {
        if (CanShoot)
        {
            myanimator.SetBool("DISP", true);
        }
        else
        {
            myanimator.SetBool("DISP", false);
        }
        

    }
   

}
