using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENemy1 : MonoBehaviour
{
    [SerializeField] AudioClip sfx_deadEn1;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject point;
    public float speedB;
    public float TimeBTShoots;
    public float ShootTIme;
    public bool EnemyDetect = false;
    [SerializeField] Transform SHposition;
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
        
        /*if (EnemyDetect)
        {
            if (Time.time >= nextFireIn)
            {
                nextFireIn = Time.time + TimeBTShoots;
                disparah();
            }
                
        }*/
        
    }
    

    bool detectarEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 5f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.left * 5f, Color.red);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right, 5f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.right * 5f, Color.blue);

        if (hit)
        {
            EnemyDetect = true;
            if (hit.collider.gameObject.tag == "Player")
            {
                if (Time.time >= nextFireIn)
                {
                    nextFireIn = Time.time + TimeBTShoots;
                    disparah();
                }
                
                transform.localScale = new Vector2(1, 1);
            }
        }
       

        if (hit2)
        {
            EnemyDetect = true;
            if (hit2.collider.gameObject.tag == "Player")
            {
                if (Time.time >= nextFireIn)
                {
                    nextFireIn = Time.time + TimeBTShoots;
                    disparah();
                }
                transform.localScale = new Vector2(-1, 1);
            }
        }
        
        return hit.collider !=null;
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        if (Hitpoints <= 0)
        {
            AudioSource.PlayClipAtPoint(sfx_deadEn1, Camera.main.transform.position);
            myanimator.SetTrigger("IsDed"); 
        }
    }
    void Die()
    {
        myanimator.SetBool("Distroy", true);
        Destroy(this);
        transform.gameObject.tag = "Untagged";
    }
    void disparah()
    { if (EnemyDetect == true)
        {
            GameObject bala = Instantiate(Bullet, SHposition.position, Quaternion.identity);
            bool direccion = transform.localScale.x == -1;
            (bala.GetComponent<EnemyBullet>()).shoot(direccion, speedB * 2);
        }
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(TimeBTShoots);
        //bool direccion = transform.localScale.x == 1;
        //(bala.GetComponent<Bullet>()).Shoot(direccion, speedB * 2);
    }

    


}
