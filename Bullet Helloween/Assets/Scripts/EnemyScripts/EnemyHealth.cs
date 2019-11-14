using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
  public int enemyhealth = 10;

    private void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Bullet")
        {
            enemyhealth--;






        }
        if (enemyhealth < 1)//health < 1
        {
            Destroy(gameObject);
        }
    }
}

