using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShadeBossAI : MonoBehaviour
{
    /// <summary>
    /// ~Shade Reaper Boss
    /// </summary>
    //VARIABLES														//VARIABLES
    [Header("General Settings")]								    //GENERAL VARIABLES
    public Transform player;                                        //Players transform location
    public GameObject prefab;										//Bullet prefab
    public GameObject boss;                                         //Boss object
    public GameObject shadeBoss;                                    //Shade boss object
    public bool bossActive = true;							        //Tells if the boss is active or not
    [Header("Base Stats Settings")]						            //VARIABLES THAT'LL BALANCE THE BOSSES BASE STATS
    public int health = 10;                                         //Health for shade boss
    public float bulletSpeed = 5f;							        //How fast the bullet will travel
    public float shootDelay = 0.5f;							        //How long the boss will wait to fire again
    public float bulletLifetime = 10.0f;				            //How long the bullet will last before being destroyed
    public float shootTimer = 0f;                                   //Timer for shooting  
    [Header("Wave Time Settings")] 								    //VARIABLES THAT'll CONTROL WHEN WAVES AND UPGRADES WILL OCCUR
    public int firstWave = 0;										//Time when the first wave will occur
    public int secondWave = 5;									    //Time when the second wave will occur
    public int thirdWave = 15;									    //Time when the third wave will occur
    public int fourthWave = 25;                                     //Time when the fourth wave will occur
    public int reset = 35;                                          //Time when the waves will reset
    public int bossPowerUp = 25;								    //When the boss reaches this amount it will have its stats upgraded
    public float waveTimer = 0f;                                    //Timer for the waves
    [Header("Shoot Pattern Settings")]                              //VARIABLES THAT'LL CONTROL SHOOTING PATTERNS
    public Vector2 startPoint;                                      //Variable for the starting position of the pattern
    public int numberOfBullets = 15;                                //How much bullets the enemy will shoot
    public float radius = 5f;                                       //Radius
    public float shootPatternTimer1Var1 = 0f;                       //Timer for shoot pattern one variation one
    public float shootPatternTimer1Var2 = 0f;                       //Timer for shoot pattern one variation two
    //START FUNCTION
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = GameObject.FindGameObjectWithTag("BossThree").gameObject;
    }
    //UPDATE FUNCTION
    void Update()
    {
        //WAVES
        if (bossActive == true)
        {
            Shoot();
        }
        if(health < 1)
        {
            boss.GetComponent<BossAI3>().shadeKill++;
            Destroy(gameObject);
        }
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            health--;
    }
    //SHOOT FUNCTION
    void Shoot()
    {
        shootTimer += Time.deltaTime;
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
    //SHOOT PATTERN(ONE)(VARIATION ONE) FUNCTION
    protected void ShootPatternOneVar1(int numberOfBullets)
    {
        shootPatternTimer1Var1 += Time.deltaTime;
        if (shootPatternTimer1Var1 > 0.5f)
        {
            startPoint = shadeBoss.GetComponent<Transform>().position;
            float angleStep = 360f / numberOfBullets;
            float angle = 0f;
            for (int i = 0; i <= numberOfBullets - 1; i++)
            {
                shootPatternTimer1Var1 = 0;
                float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
                Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * bulletSpeed;
                var bullet = Instantiate(prefab, startPoint, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
                Destroy(bullet, bulletLifetime);
                angle += angleStep;
            }
        }
    }
    //SHOOT PATTERN(ONE)(VARIATION TWO) FUNCTION
    protected void ShootPatternOneVar2(int numberOfBullets)
    {
        shootPatternTimer1Var2 += Time.deltaTime;
        if (shootPatternTimer1Var2 > 1.25f)
        {
            startPoint = shadeBoss.GetComponent<Transform>().position;
            float angleStep = 360f / numberOfBullets;
            float angle = 180f;
            for (int i = 0; i <= numberOfBullets - 1; i++)
            {
                shootPatternTimer1Var2 = 0;
                float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
                Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * bulletSpeed;
                var bullet = Instantiate(prefab, startPoint, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
                Destroy(bullet, bulletLifetime);
                angle += angleStep;
            }
        }
    }
}
///END OF SCRIPT!