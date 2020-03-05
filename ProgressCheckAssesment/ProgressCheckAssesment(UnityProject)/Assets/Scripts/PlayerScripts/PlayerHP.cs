using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour
{
    //VARIABLES
    int health = 5;
    Slider healthSlider;
    //START FUNCTION
    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    //FUNCTION
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            health--;
            healthSlider.value = health;
        }
    }
}
