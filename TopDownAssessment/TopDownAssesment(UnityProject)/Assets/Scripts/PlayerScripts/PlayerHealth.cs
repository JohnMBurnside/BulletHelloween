using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    //VARIABLES
    public int health = 5;
    public int lives = 3;
    public Slider healthSlider;
    public int newValue = 0;
    //START FUNCTION
    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    //UPDATE FUNCTION
    void Update()
    {
        if(health < 1)
        {
            newValue = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        /*if(newValue == 1 && Input.GetKeyDown(KeyCode.Space))
        /{
            SceneManager.LoadScene("LevelOne");
            newValue = 0;
        }*/
    }
    //TRIGGER FUNCTION(BULLET)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Bullet")
        {
            health--;
            healthSlider.value = health;
        }
    }
}
