using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
	//VARIABLES
	public int bossHealth = 50;
	public Slider bossSlider = bossHealth;
	//START FUNCTION
	void Start()
	{
		GetComponent<canvas>().enabled = false;
		bossSlider.maxValue = bossHealth;
		bossSlider.Value = bossHealth;
	}
	//UPDATE FUNCTION
	void Update()
	{
		if(bossHealth < 1)
			Destroy(gameObject);
	}
	//TRIGGER FUNCTION
	void OnTriggerEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
			GetComponent<canvas>().enabled = true;
	}
	//COLLISION FUNCTION
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Bullet")
		{
			bossHealth--;
			bossSlider.Value = bossHealth;
		}
	}
}
