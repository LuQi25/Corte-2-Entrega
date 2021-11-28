using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy2 : MonoBehaviour
{
    public Animator myANimator;
    public float Hitpoints;
    public float MaxHitPoints = 5; //vida max
    
    void Start()
    {
        myANimator = GetComponent<Animator>();
        Hitpoints = MaxHitPoints;
    }

    
    void Update()
    {
        
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        if (Hitpoints <= 0)
        {
            myANimator.SetBool("Dying", true);
            Destroy(gameObject,10);
        }
    }

    void Muertito()
    {
        
    }

}
