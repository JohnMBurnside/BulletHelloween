using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [Header("Notes")]                               //NOTES
    [TextArea]                                      //TAG NOTES
    [Tooltip("Notes for what to name tags")]        //TAG NOTES
    public string TagNotes =                        //TAG NOTES
    "TAG NAMES\n" +                                 //TAG NOTES
    "Enemies should be tagged as 'Enemy'\n" +       //TAG NOTES
    "Projectiles should be tagged as 'Bullet'\n" +  //TAG NOTES
    "Health Potion Tag is 'Health Potion'\n" +      //TAG NOTES
    "Shield Potion Tag is 'Shield Potion'\n" +      //TAG NOTES    
    "Power Up Potion Tag is 'Power Potion'\n";      //TAG NOTES
    [TextArea]                                      //SCENE NAMES
    [Tooltip("Notes for what to name scenes")]      //SCENE NAMES
    public string SceneNotes =                      //SCENE NAMES
    "SCENE NAMES\n" +                               //SCENE NAMES
    "Game Over Scene is 'Game Over'\n";             //SCENE NAMES
    //VARIABLES                                     //VARIABLES
    [Header("General Variables")]                   //GENERAL VARIABLES
    public GameObject player;                       //Object variable for player
    [Header("Health Settings")]                     //VARIABLES FOR HEALTH
    [Tooltip("Max health variable")]                //Hover over variable for tooltip
    public int maxHealth = 5;                       //Max health variable
    [Tooltip("Health variable")]                    //Hover over variable for tooltip
    public int health = 5;                          //Health variable
    [Tooltip("Health slider")]                      //Hover over variable for tooltip
    public Slider healthSlider;                     //Health slider
    [Header("Shield Settings")]                     //VARIABLES FOR SHIELD
    [Tooltip("Shield variable")]                    //Hover over variable for tooltip
    public int shield = 5;                          //Shield variable
    [Tooltip("Shield slider")]                      //Hover over variable for tooltip
    public Slider shieldSlider;                     //Shield slider
    [Header("Life Settings")]                       //VARIABLES FOR LIVES
    [Tooltip("Lives variable")]                     //Hover over variable for tooltip
    public int lives = 3;                           //Lives variable
    [Tooltip("Lives text")]                         //Hover over variable for tooltip
    public Text livesText;                          //Lives text
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
        //LIVES
        lives = PlayerPrefs.GetInt("lives");
        livesText.text = "x" + lives;
    }
    //UPDATE FUNCTION
    void Update()
    {
        if (lives < 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        if (powerUpTimer > powerUpTime)
            player.GetComponent<PlayerShoot>().shootDelay = 0.5f;
    }
    //COLLISION FUNCTION                           
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
        else if (collision.gameObject.tag == "Health Potion")
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
            Destroy(collision.gameObject);
            PowerUp();
        }
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
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            PlayerPrefs.SetInt("lives", lives - 1);
                        }
                    }
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