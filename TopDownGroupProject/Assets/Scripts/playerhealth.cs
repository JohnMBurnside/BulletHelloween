using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    //VARIABLES
    //HEALTH
    public int health = 5;
    public Slider healthSlider;
    //SHIELD
    public int shield = 5;
    public Slider shieldSlider;
    //LIVES
    public int lives = 3;
    public Text livesText;
    //START FUNCTION
    void Start()
    {
        //HEALTH
        healthSlider.maxValue = health;
        healthSlider.value = health;
        //SHIELD
        shieldSlider.maxValue = shield;
        shieldSlider.value = shield;
        //LIVES
        lives = PlayerPrefs.GetInt("lives");
        livesText.text = "x" + lives;
    }
    void Update()
    {
        //PlayerPrefs.SetInt("lives", 0);
        if (lives < 0)
        {
            
            SceneManager.LoadScene("Game Over");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy")
        {
            if (shield > 0)
            {
                shield--;
                shieldSlider.value = shield;
            }
            else if (shield < 1)
            {
                if (health > 0)
                {
                    health--;
                    healthSlider.value = health;
                    if (health < 1)
                    {
                        if (lives > -1)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            PlayerPrefs.SetInt("lives", lives - 1);
                        }
                    }
                }
            }
        }
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy")
        {
            if (shield > 0)
            {
                shield--;
                shieldSlider.value = shield;
            }
            else if (shield < 1)
            {
                if (health > 0)
                {
                    health--;
                    healthSlider.value = health;
                    if (health < 1)
                    {
                        if (lives > -1)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            PlayerPrefs.SetInt("lives", lives - 1);
                        }
                    }
                }
            }
        }
    }
}