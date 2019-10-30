using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossHealthBar : MonoBehaviour
{
    //VARIABLES
    public GameObject bossHUD;
    public GameObject boss;
    public bool barOn = false;
    //START FUNCTION
    void Start()
    {
        bossHUD.GetComponent<Canvas>().enabled = false;
        //boss.GetComponent<BossAI>().bulletLifetime = 0;
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && barOn == false)
        {
            barOn = true;
            bossHUD.GetComponent<Canvas>().enabled = true;
            //boss.GetComponent<BossAI>().bossActive = true;
           // boss.GetComponent<BossAI>().bulletLifetime = 10;
        }
        else if (collision.gameObject.tag == "Player" && barOn == true)
        {
            barOn = false;
            bossHUD.GetComponent<Canvas>().enabled = false;
           // boss.GetComponent<BossAI>().bossActive = false;
           // boss.GetComponent<BossAI>().bulletLifetime = 0;
        }
    }
}
