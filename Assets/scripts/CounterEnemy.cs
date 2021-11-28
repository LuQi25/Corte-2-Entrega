using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CounterEnemy : MonoBehaviour
{
    bool gamePaused = false;
    [SerializeField] GameObject WInUI;
    public bool win;
    GameObject[] enemies;
    public Text enemyCountText;
    

    void Start()
    {
        WInUI.SetActive(false);
    }

    
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCountText.text = "Enemigos :" + enemies.Length.ToString();
        if (enemies.Length <= 0)
        {
            WiNgame();
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void WiNgame()
    {
        gamePaused = gamePaused ? false : true;
        WInUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }
    
}
