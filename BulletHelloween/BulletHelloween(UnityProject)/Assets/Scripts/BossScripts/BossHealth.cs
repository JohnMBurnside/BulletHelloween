using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    //VARIABLES                                 //VARIABLES
    [Header("Genral Settings")]                 //GENERAL VARIABLES
    public Slider bossSlider;                   //Boss slider
    public GameObject bossHUD;                  //Boss HUD
    public GameObject healthPotion;             //Health potion object
    public GameObject bounds;                   //Bounds object
    [Header("Health Settings")]                 //HEALTH VARIABLES
    public int maxBossHealth = 50;              //Max health for the boss
    public int bossHealth = 50;                 //Boss health
    [Header("Sound Settings")]                  //SOUND VARIABLES
    public bool BruhSoundEffect2 = false;       //Tells whether to play Bruh Sound Effect #2
    //START FUNCTION
    void Start()
    {
        bossSlider.maxValue = bossHealth;
        bossSlider.value = bossHealth;
    }
    //UPDATE FUNCTION
    void Update()
    {
        if (bossHealth < 1)
        {
            BruhSoundEffect2 = true;    
            bounds.GetComponent<EdgeCollider2D>().enabled = false;
            bossHUD.GetComponent<Canvas>().enabled = false;
            Instantiate(healthPotion, transform.position, Quaternion.identity);
            BruhSoundEffect2 = false;
            Destroy(gameObject);
        }
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            bossHealth--;
            bossSlider.value = bossHealth;
        }
    }
}
///END OF SCRIPT!