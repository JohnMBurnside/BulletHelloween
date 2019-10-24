using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
	//VARIABLES
	public int health = 5;
	public Slider healthSlider = health;
	//START FUNCTION
	void Start()
	{
		healthSlider.maxValue = health;
		healthSlider.Value = health;
	}
	//UPDATE FUNCTION
	void Update()
	{
		if(health < 1)
			SceneManager.LoadScene(SceneManger.GetSceneActive().name)
		else if (lives < 0)
			SceneManager.LoadScene("Game Over");
	}
	//COLLISION FUNCTION
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gamObject.tag == "Bullet")
		{
			health--;
			healthSlider.Value = health;
		}
	}	
}
