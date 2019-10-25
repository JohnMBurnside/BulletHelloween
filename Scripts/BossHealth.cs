uusing System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
	//VARIABLES
	public int bossHealth = 50;
    public Slider bossSlider;
    ///public GameObject bossHUD;
    //START FUNCTION
    void Start()
	{
        ///bossHUD.GetComponent<Canvas>().enabled = false;
		bossSlider.maxValue = bossHealth;
		bossSlider.value = bossHealth;
	}
	//UPDATE FUNCTION
	void Update()
	{
		if(bossHealth < 1)
        {
            Destroy(gameObject);
            ///bossHUD.GetComponent<Canvas>().enabled = false;
        }
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        ///if (collision.gameObject.tag == "Player")
            ///bossHUD.GetComponent<Canvas>().enabled = true;
        ///else if (collision.gameObject.tag == "Player")
            ///bossHUD.GetComponent<Canvas>().enabled = false;
    }
    //COLLISION FUNCTION
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            bossHealth--;
            bossSlider.value = bossHealth;
        }
    }
}
