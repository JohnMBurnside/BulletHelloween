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
    //public GameObject healthHUD;
    //SHIELD
    public int shield = 5;
    public Slider shieldSlider;
    //public GameObject shieldHUD;
    //LIVES
    public int lives = 3;
    public Text livesText;
    //START FUNCTION
    void Start()
    {
        //HEALTH
        healthSlider.maxValue = health;
        healthSlider.value = health;
        //healthHUD.GetComponent<Canvas>().enabled = true;
        //SHIELD
        shieldSlider.maxValue = shield;
        shieldSlider.value = shield;
        //shieldHUD.GetComponent<Canvas>().enabled = true;
        //LIVES
        lives = PlayerPrefs.GetInt("lives", lives);
        livesText.text = "x" + lives;
    }
    //UPDATE FUNCTION
    void Update()
    {

    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
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
                            PlayerPrefs.SetInt("lives", lives - 1);
                            livesText.text = "x" + lives;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        else if (lives > 0)
                            SceneManager.LoadScene("Game Over");
                    }
                }
            }
        }
    }
}
