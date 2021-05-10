using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel;
    public GameObject gameOverPanel;
    public Slider healthBar;
    //public GameObject player;
    public HealthScript healthScript;


    public static int score = 0;
    public static bool gameOver;
    public static bool winLevel;
    public float timer = 0;
    private int healthPoints;

    void Start()
    {
        gameOver = winLevel = false;
        //healthScript = GetComponent<HealthScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + score;       
        healthBar.value = healthScript.currentHealth;
        if(healthScript.characterDied == true)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
        }
        // если расстояние до ближайшего врага нулевое(то бишь их больше нет)
        else if( FindObjectsOfType<Enemy>().Length == 0) 
        {
            winLevel = true;
            //player.GetComponent<PlayerController>().anim.SetBool("OnGround", false);
            timer += Time.deltaTime;
            if(timer > 5)
            {
                int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextLevel == 3)
                    SceneManager.LoadScene(0);

                if(PlayerPrefs.GetInt("ReachedLevel", 1) < nextLevel)
                    PlayerPrefs.SetInt("ReachedLevel", nextLevel);

                SceneManager.LoadScene(nextLevel);
            }

        }
    }
}
