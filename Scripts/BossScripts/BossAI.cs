using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossAI : MonoBehaviour
{
    //VARIABLES																	//VARIABLES
    //GENERAL VARIABLES													//GENERAL VARIABLES
    public Transform player;										//Players transform location
    public GameObject prefab;										//Bullet prefab
    public GameObject boss;											//Boss object
    public bool bossActive = false;							//Tells if the boss is active or not
    //BALANCE(BASE) VARIABLES										//VARIABLES THAT'LL BALANCE THE BOSSES BASE STATS
    public float bulletSpeed = 5f;							//How fast the bullet will travel
    public float shootDelay = 0.5f;							//How long the boss will wait to fire again
    public float bulletLifetime = 10.0f;				//How long the bullet will last before being destroyed
    public float shootTimer = 0f;								//Timer for shooting
		//BALANCE(UPGRADE) VARIABLES								//VARIABLES THAT'LL BALANCE THE BOSSES UPGRADED STATS
		public float bulletSpeedUpgrade = 2f;				//How fast the bullet will travel after the upgrade
		public float shootDelayUpgrade = 0.3f;			//How long the boss will wait to fire again after the upgrade
    //PATTERN WAVE VARIABLES										//VARIABLES THAT'll CONTROL WHEN WAVES AND UPGRADES WILL OCCUR
    public int firstWave = 0;										//Time when the first wave will occur
    public int secondWave = 5;									//Time when the second wave will occur
    public int thirdWave = 15;									//Time when the third wave will occur
    public int reset = 30;											//Time when the waves will reset
    public int bossPowerUp = 25;								//When the boos reaches this amount it will have its stats upgraded
    public float waveTimer = 0f;								//Timer for the waves
    //UPDATE FUNCTION														//UPDATE: Called every frame
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
    //SHOOT FUNCTION														//SHOOT: When called, creates a bullet
    void Shoot()
    {
    		shootTimer += Time.deltaTime;
				if(boss.GetComponent<BossHealth>().bosshealth < bossPowerUp)
				{
						shootDelay = shootDelayUpgrade
						if (shootTimer > shootDelayUpgrade)
						{
								shootTimer = 0;
								GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
								Vector3 playerPosition = player.position;
								Vector2 shootDir = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
								shootDir.Normalize();
								bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeedUpgrade;
								Destroy(bullet, bulletLifetime);
						}
				}
				else if (time > shootDelay)
				{
            if (shootTimer > shootDelay)
            {
							  shootTimer = 0;
                GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
								Vector3 playerPosition = player.position;
								Vector2 shootDir = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
								shootDir.Normalize();
								bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
								Destroy(bullet, bulletLifetime);
            }
        }
    }
    //SHOOT PATTERN(ONE) FUNCTION								//SHOOT PATTERN(ONE): Pattern for bullet paths
    void ShootPatternOne()
    {
    }
    //SHOOT PATTERN(TWO) FUNCTION								//SHOOT PATTERN(TWO): Pattern for bullet paths
    void ShootPatternTwo()
    {
    }
}
