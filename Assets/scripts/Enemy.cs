using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] CircleCollider2D Detector;
    [SerializeField] GameObject player;
    [SerializeField] AudioClip sfx_deadEn2;
    public Animator myanimator;
    public float MaxHitPoints = 5;
    public float Hitpoints;

    void Start()
    {
        myanimator = GetComponent<Animator>();
        Hitpoints = MaxHitPoints;
    }

    
    void Update()
    {
        //Metodo recomendable --malo que esta aun en update y no es evento.  -update que va cada frame-.
        Collider2D chocando = Physics2D.OverlapCircle(transform.position, 6, LayerMask.GetMask("Player"));

        if (chocando !=null)
        {
            //Debug.Log("Siguiendo el pj");
        }
        else
        {
            //Debug.Log("No siga al pj");
        }


        /* Metodo 2
         * if (Vector2.Distance(transform.position, player.transform.position)<10) // solucion 2 sirve entre 2 puntos y no varios.
        {
            Debug.Log("Siguiendo el pj");
        }
        else
        {
            Debug.Log("No seguir");
        }*/


        /*Metodo 1
         * if (Detector.IsTouchingLayers(LayerMask.GetMask("Player")))    //solucion 1 sirve mucho.
        {
            Debug.Log("Siguiendo el pj");
        }
        else
        {
            Debug.Log("Dejar de seguir");
        }*/

        
 
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, player.transform.position); //con la solucion 2.
        Gizmos.DrawWireSphere(transform.position, 6);
    }
    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        if (Hitpoints <= 0)
        {
            AudioSource.PlayClipAtPoint(sfx_deadEn2, Camera.main.transform.position);
            myanimator.SetBool("Dying", true);

        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


}
