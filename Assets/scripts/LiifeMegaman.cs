using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LiifeMegaman : MonoBehaviour
{
    [SerializeField] GameObject FailUI;
    [SerializeField] AudioClip sfx_deadMegaman;

    public bool lose;
    public float Hitpoints;
    public float MaxHitPoints = 10; //vida max
    public float dmg = 2;
    void Start()
    {
        Hitpoints = MaxHitPoints;
        FailUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDMG(float damage)
    {
        Hitpoints -= damage;
        if (Hitpoints <= 0)
        {
            GameOver();
            Destroy(gameObject);
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }
    public void GameOver()
    {
        FailUI.SetActive(true);
        Time.timeScale = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            Hitpoints-=dmg;
            if (Hitpoints <= 0)
            {
                AudioSource.PlayClipAtPoint(sfx_deadMegaman, Camera.main.transform.position);
                GameOver();
                Destroy(gameObject);
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        if (collision.gameObject.layer == 10)
        {
            Hitpoints-=dmg;
            if (Hitpoints <= 0)
            {
                AudioSource.PlayClipAtPoint(sfx_deadMegaman, Camera.main.transform.position);
                GameOver();
                Destroy(gameObject);
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

}
