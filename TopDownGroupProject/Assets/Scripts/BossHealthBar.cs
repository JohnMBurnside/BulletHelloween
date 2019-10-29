using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossHealthBar : MonoBehaviour
{
    //VARIABLES
    public GameObject bossHUD;
    public bool barOn = false;
    //START FUNCTION
    void Start()
    {
        bossHUD.GetComponent<Canvas>().enabled = false;
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && barOn == false)
        {
            barOn = true;
            bossHUD.GetComponent<Canvas>().enabled = true;
        }
        else if (collision.gameObject.tag == "Player" && barOn == true)
        {
            barOn = false;
            bossHUD.GetComponent<Canvas>().enabled = false;
        }
    }
}
