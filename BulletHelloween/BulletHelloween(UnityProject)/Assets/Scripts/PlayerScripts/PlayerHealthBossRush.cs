using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealthBossRush : MonoBehaviour
{
    /// <summary>
    /// ~Player Health(Boss Rush)
    ///     -Only diffrence from the regular player health is there is no lives
    /// </summary>
    //VARIABLES                                     //VARIABLES
    [Header("General Variables")]                   //GENERAL VARIABLES
    public GameObject player;                       //Object variable for player
    [Header("Health Settings")]                     //VARIABLES FOR HEALTH
    public int maxHealth = 5;                       //Max health variable
    public int health = 5;                          //Health variable
    public Slider healthSlider;                     //Health slider
    [Header("Shield Settings")]                     //VARIABLES FOR SHIELD
    public int maxShield = 5;                       //Max shield variable
    public int shield = 5;                          //Shield variable
    public Slider shieldSlider;                     //Shield slider
    [Header("Power Up Variables")]                  //GENERAL VARIABLES
    public float powerUpTime = 10;                  //How long a power up is active for
    public float powerUpTimer = 0f;                 //Timer
    //START FUNCTION
    void Start()
    {
        //HEALTH
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        //SHIELD
        shieldSlider.maxValue = shield;
        shieldSlider.value = shield;
    }
    //UPDATE FUNCTION
    void Update()
    {
        if (health < 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (powerUpTimer > powerUpTime)
            player.GetComponent<PlayerShoot>().shootDelay = 0.5f;
        if (health > maxHealth)
            health = maxHealth;
        if (shield > maxShield)
            shield = maxShield;
    }
    //COLLISION FUNCTION                           
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Health Potion")
        {
            maxHealth++;
            health = maxHealth;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Shield Potion")
        {
            shield = shield + 2;
            shieldSlider.value = shield;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Power Potion")
        {
            PowerUp();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Full Shield Potion")
        {
            shield = maxShield;
            shieldSlider.value = shield;
            Destroy(collision.gameObject);
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
                }
            }
        }
    }
    //POWER UP FUNCTION
    void PowerUp()
    {
        powerUpTimer = 0;
        GetComponent<PlayerShoot>().shootDelay = GetComponent<PlayerShoot>().shootDelayUpgrade;
    }
}
///END OF SCRIPT!