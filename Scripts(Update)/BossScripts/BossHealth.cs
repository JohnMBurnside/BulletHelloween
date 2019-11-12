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
    [Header("Health Settings")]                 //HEALTH VARIABLES
    public int maxBossHealth = 50;              //Max health for the boss
    public int bossHealth = 50;                 //Boss health
    //START FUNCTION
    void Start()
	{
		bossSlider.maxValue = bossHealth;
		bossSlider.value = bossHealth;
	}
	//UPDATE FUNCTION
	void Update()
	{
		if(bossHealth < 1)
        {
            Destroy(gameObject);
            bossHUD.GetComponent<Canvas>().enabled = false;
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