using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Animator myanimator;
    public float Hitpoints;
    public float MaxHitPoints = 5; //vida max
    void Start()
    {
        myanimator = GetComponent<Animator>();
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
            myanimator.SetTrigger("IsDed");
            
        }
    }
    
    void Die()
    {
        myanimator.SetBool("Distroy",true);
    }





}
