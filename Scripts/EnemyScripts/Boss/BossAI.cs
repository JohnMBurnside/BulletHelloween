using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossAI : MonoBehaviour
{
    //VARIABLES
    //GENERAL VARIABLES
    public Transform player;
    public GameObject prefab;
    public GameObject boss;
    public bool bossActive = false;
    //BALANCE VARIABLES
    public float bulletSpeed = 5f;
    public float bulletSpeedUpgrade = 2f;
    public float shootDelay = 0.5f;
    public float shootDelayUpgrade = 0.3f;
    public float bulletLifetime = 10.0f;
    public float timer = 0f;
    //PATTERN WAVE VARIABLES
    public int firstWave = 0;
    public int secondWave = 5;
    public int thirdWave = 15;
    public int reset = 30;
    public int bossPowerUp = 25;
    public float waveTimer = 0f;
    //UPDATE FUNCTION
    void Update()
    {
        //WAVES
        if(bossActive == true)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer > firstWave && waveTimer < secondWave)
                Shoot();
            else if (waveTimer > secondWave && waveTimer < thirdWave)
                ShootPatternOne();
            else if (waveTimer > thirdWave && waveTimer < reset)
                ShootPatternTwo();
            else
                waveTimer = 0;
        }
    }
    //SHOOT FUNCTION
    void Shoot()
    {
        {
            timer += Time.deltaTime;
            if (timer > shootDelay)
            {
                if (boss.GetComponent<BossHealth>().bossHealth < bossPowerUp)
                {
                    timer = 0;
                    GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
                    Vector3 playerPosition = player.position;
                    Vector2 shootDir = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
                    shootDir.Normalize();
                    bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeedUpgrade;//PROBLEM HERE TO FIX
                    Destroy(bullet, bulletLifetime);
                }
                else
                {
                    timer = 0;
                    GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
                    Vector3 playerPosition = player.position;
                    Vector2 shootDir = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
                    shootDir.Normalize();
                    bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
                    Destroy(bullet, bulletLifetime);
                }

            }
        }
    }
    //SHOOT PATTERN(ONE) FUNCTION 
    void ShootPatternOne()
    {
    }
    //SHOOT PATTERN(TWO) FUNCTION
    void ShootPatternTwo()
    {
    }
}
