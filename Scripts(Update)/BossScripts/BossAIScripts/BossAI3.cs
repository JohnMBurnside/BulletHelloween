using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossAI3 : MonoBehaviour
{
    /// <summary>
    /// ~Reaper Boss
    /// </summary>
    //VARIABLES	
    [Header("General Settings")]                                                //GENERAL VARIABLES
    public Transform player;                                                    //Players transform location
    public Transform bossLocation;                                              //Bosses transform location
    public GameObject prefab;                                                   //Bullet prefab
    public GameObject boss;                                                     //Boss object
    public GameObject bossShadow;                                               //Bosses shadow version
    public bool bossActive = false;                                             //Tells if the boss is active or not
    [Header("Base Stats Settings")]                                             //VARIABLES THAT'LL BALANCE THE BOSSES BASE STATS
    public float bulletSpeed = 5f;                                              //How fast the bullet will travel
    public float shootDelay = 0.5f;                                             //How long the boss will wait to fire again
    public float bulletLifetime = 10.0f;                                        //How long the bullet will last before being destroyed
    float shootTimer = 0f;                                                      //Timer for shooting  
    [Header("Wave Time Settings")]                                              //VARIABLES THAT'll CONTROL WHEN WAVES AND UPGRADES WILL OCCUR
    public int firstWave = 0;                                                   //Time when the first wave will occur
    public int secondWave = 5;                                                  //Time when the second wave will occur
    public int thirdWave = 15;                                                  //Time when the third wave will occur
    public int reset = 25;                                                      //Time when the waves will reset
    float waveTimer = 0f;                                                       //Timer for the waves
    [Header("Shoot Pattern Settings")]                                          //VARIABLES THAT'LL CONTROL SHOOTING PATTERNS
    public Vector2 startPoint;                                                  //Variable for the starting position of the pattern
    public int numberOfBullets = 15;                                            //How much bullets the enemy will shoot
    public float radius = 5f;                                                   //Radius
    public float angle = 180f;                                                  //Angle of bullet direction
    float shootPatternTimer1Var1 = 0f;                                          //Timer for shoot pattern one variation one
    float shootPatternTimer1Var2 = 0f;                                          //Timer for shoot pattern one variation two
    float shootPatternTimer1Var3 = 0f;                                          //Timer for shoot pattern one variation three
    [Header("Shaodw Settings")]                                                 //VARIABLES FOR WHERE THE BOSS WILL SPAWN ITS SHADE
    public Vector2 safePoint = new Vector2(0, +100);                            //Safe point for boss to teleport to
    public Vector2 shade1 = new Vector2(-5, +5);                                //Where the boss will spawn its shade
    public Vector2 shade1Point;                                                 //Shade area 1 position
    public Vector2 shade2 = new Vector2(-5, -5);                                //Where the boss will spawn its shade
    public Vector2 shade2Point;                                                 //Shade area 2 position
    public Vector2 shade3 = new Vector2(+5, -5);                                //Where the boss will spawn its shade
    public Vector2 shade3Point;                                                 //Shade area 3 position
    public Vector2 shade4 = new Vector2(+5, +5);                                //Where the boss will spawn its shade
    public Vector2 shade4Point;                                                 //Shade area 4 position
    public Vector2 homePoint;                                                   //Boss home point
    public int hitCounter = 0;                                                  //How much times the boss has been hit
    public int shadeCondition;                                                  //How much time the boss is hit for the shades to spawn
    public int shadeKill = 4;                                                   //How many shade you killed
    public int shade = 0;                                                       //Variable for Shadow function
    //START FUNCTION
    void Start()
    {
        homePoint = boss.GetComponent<Transform>().position;
        shadeCondition = boss.GetComponent<BossHealth>().maxBossHealth / 4;
        startPoint = boss.GetComponent<Transform>().position;
        shade1Point = new Vector2(startPoint.x + shade1.x, startPoint.y + shade1.y);
        shade2Point = new Vector2(startPoint.x + shade2.x, startPoint.y + shade2.y);
        shade3Point = new Vector2(startPoint.x + shade3.x, startPoint.y + shade3.y);
        shade4Point = new Vector2(startPoint.x + shade4.x, startPoint.y + shade4.y);
    }
    //UPDATE FUNCTION
    void Update()
    {
        startPoint = boss.GetComponent<Transform>().position;
        //TESTING
        if (bossActive == true)
        {
            if (hitCounter > shadeCondition)
            {
                if(shade < 4)
                {
                    Shadow();
                    shade++;
                }
                else
                    hitCounter = 0;
            }
            if (shadeKill == 4)
            {
                GetComponent<Transform>().position = homePoint;
                shade = 0;
            }
            else
                GetComponent<Transform>().position = safePoint;
        }
        //WAVES
        if (bossActive == true)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer > firstWave && waveTimer < secondWave)
                Shoot();
            else if (waveTimer > secondWave && waveTimer < thirdWave)
            {
                ShootPatternOneVar1(numberOfBullets);
                ShootPatternOneVar2(numberOfBullets);
            }
            else if (waveTimer > thirdWave && waveTimer < reset)
            {
                ShootPatternOneVar1(numberOfBullets);
                ShootPatternOneVar2(numberOfBullets);
                ShootPatternOneVar3(numberOfBullets);
            }
            else
                waveTimer = 0;
        }
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            hitCounter++;
    }
    //SHADOW FUNCTION
    void Shadow()
    {
        shadeKill = 0;
        if (shade == 0)
            Instantiate(bossShadow, shade1Point, Quaternion.identity);
        else if (shade == 1)
            Instantiate(bossShadow, shade2Point, Quaternion.identity);
        else if (shade == 2)
            Instantiate(bossShadow, shade3Point, Quaternion.identity);
        else if (shade == 3)
            Instantiate(bossShadow, shade4Point, Quaternion.identity);
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
    void ShootPatternOneVar1(int numberOfBullets)
    {
        shootPatternTimer1Var1 += Time.deltaTime;
        if (shootPatternTimer1Var1 > 0.5f)
        {
            float angleStep = 360f / numberOfBullets;
            angle = 0f;
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
    void ShootPatternOneVar2(int numberOfBullets)
    {
        shootPatternTimer1Var2 += Time.deltaTime;
        if (shootPatternTimer1Var2 > 1.25f)
        {
            float angleStep = 360f / numberOfBullets;
            angle = 180;
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
    //SHOOT PATTERN(ONE)(VARIATION THREE) FUNCTION
    void ShootPatternOneVar3(int numberOfBullets)
    {
        shootPatternTimer1Var3 += Time.deltaTime;
        if (shootPatternTimer1Var3 > 0.5f)
        {
            float angleStep = 360f / numberOfBullets;
            angle = 90f;
            for (int i = 0; i <= numberOfBullets - 1; i++)
            {
                shootPatternTimer1Var3 = 0;
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