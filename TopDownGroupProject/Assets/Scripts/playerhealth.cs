using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playerhealth : MonoBehaviour
{
    public int health = 10;
    public Text healthText;
    public Slider healthSlider;
    public int lives = 10;
     void Start()
    {
        healthText.text = "Health: " + health;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        //PlayerPrefs.SetInt("lives", lives);
        lives = PlayerPrefs.GetInt("lives");
    }
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            health--;
            healthText.text = "Health:" + health;
            healthSlider.value = health;
            //same as health -= 1, or health = health
            if(health < 1)
            {
                if (lives > 0)
                {
                    SceneManager.LoadScene(
                        SceneManager.GetActiveScene().name);
                    //SceneManager.LoadScene("Lose");
                    PlayerPrefs.SetInt("lives", lives - 1);
                }
                else
                {
                    SceneManager.LoadScene("Lose");
                }
            }
        }
    }
    private void OnCollsionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            healthText.text = "Health:" + health;
            healthSlider.value = health;
            //same as health -= 1, or health = health
            if (health < 1)
            {
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().name);
                //SceneManager.LoadScene("Lose");

            }
        }
    }
}
