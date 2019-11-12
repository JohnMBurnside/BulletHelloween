using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossAI4 : MonoBehaviour
{
    /// <summary>
    /// ~Headless Boss
    ///     -This boss will be the fourth and last boss with the most health and will combine all boss mechanics thus far
    ///     -Mechanics:
    ///         -The boss will spawn 2 shadow bosses every time its health goes down a quarter(1/4)
    ///         -You'll have to kill all 2 shadow bosses to move on(25 health each)
    ///         -In total you'll go through 6 shadow bosses
    ///         -The boss will also have the teleport mechanic(teleports every 25 hits)
    ///     -Waves(Boss):
    ///         -The first wave will be the first and second pattern
    ///         -The second wave will do the first, second and third pattern with bullets slowed down to 3(speed = 3)
    ///         -The third wave will be an entirely new pattern
    ///         -The fourth wave will be the entirely new pattern but the second variation
    ///         -Repeat
    ///     -Waves(Shadow Boss):
    ///         -The shadow boss will only shoot and target the player
    /// </summary>
    //VARIABLES														//VARIABLES
    [Header("General Settings")]								    //GENERAL VARIABLES
    public Transform player;                                        //Players transform location
    public GameObject prefab;										//Bullet prefab
    public GameObject boss;                                         //Boss object
    public Transform bossLocation;                                  //Bosses transform location
    public GameObject bossShadow;                                   //Shadow boss object
    public bool bossActive = false;							        //Tells if the boss is active or not
    [Header("Base Stats Settings")]						            //VARIABLES THAT'LL BALANCE THE BOSSES BASE STATS
    public float bulletSpeed = 5f;							        //How fast the bullet will travel
    public float shootDelay = 0.5f;							        //How long the boss will wait to fire again
    public float bulletLifetime = 10.0f;				            //How long the bullet will last before being destroyed
    float shootTimer = 0f;                                          //Timer for shooting  
    [Header("Upgraded Stats Settings")]					            //VARIABLES THAT'LL BALANCE THE BOSSES UPGRADED STATS
    public float bulletSpeedUpgrade = 10f;                          //How fast the bullet will travel after the upgrade
    public float shootDelayUpgrade = 0.3f;			                //How long the boss will wait to fire again after the upgrade
    [Header("Wave Time Settings")] 								    //VARIABLES THAT'll CONTROL WHEN WAVES AND UPGRADES WILL OCCUR
    public int firstWave = 0;										//Time when the first wave will occur
    public int secondWave = 5;									    //Time when the second wave will occur
    public int thirdWave = 10;									    //Time when the third wave will occur
    public float fourthWave = 12.5f;                                //Time when the fourth wave will occur
    public int reset = 15;                                          //Time when the waves will reset
    float waveTimer = 0f;                                           //Timer for the waves
    [Header("Shoot Pattern Settings")]                              //VARIABLES THAT'LL CONTROL SHOOTING PATTERNS
    public Vector2 startPoint;                                      //Variable for the starting position of the pattern
    public int numberOfBullets = 15;                                //How much bullets the enemy will shoot
    public float radius = 5f;                                       //Radius
    public float angle = 180f;                                      //Angle of bullet direction
    float shootPatternTimer1Var1 = 0f;                              //Timer for shoot pattern one variation one
    public float shootPatternDelayVar1 = 0.5f;                      //Shoot delay for pattern
    float shootPatternTimer1Var2 = 0f;                              //Timer for shoot pattern one variation two
    public float shootPatternDelayVar2 = 1.25f;                     //Shoot delay for pattern
    float shootPatternTimer1Var3 = 0f;                              //Timer for shoot pattern one variation three
    public float shootPatternDelayVar3 = 0.5f;                      //Shoot delay for pattern
    float newTimer;                                                 //New Timer
    [Header("Teleport Settings")]                                   //TELEPORT VARIABLES
    public bool home = true;                                        //Checks whether the boss is in the home position
    public Vector2 homePoint;                                       //Home position
    public bool teleportArea1;                                      //Checks whether the boss is in the first teleport area position
    public Vector2 teleportArea1Point;                              //Teleport area 1 position
    public bool teleportArea2;                                      //Checks whether the boss is in the second teleport area position
    public Vector2 teleportArea2Point;                              //Teleport area 2 position
    public float teleportTimer;                                     //Teleport timer
    public float teleportDelay = 0.5f;                              //Teleport delay
    [Header("Teleport Condition Settings")]                         //VARIABLES THAT'll CONTROL WHEN WAVES AND UPGRADES WILL OCCUR
    public int hitCounter = 0;                                      //Measures how much times the enemy has been hit
    public int hitCounterTeleport = 25;                             //How much times the enemy will be hit before teleporting
    [Header("Teleport Direction Settings")]                         //VARIABLES FOR WHERE THE TELEPORTING WILL TAKE PLACE
    public Vector2 teleport1 = new Vector2(+3, 0);                  //How far the enemy will teleport the first time
    public Vector2 teleport2 = new Vector2(-3, 0);                  //How far the enemy will teleport the second time
    [Header("Shadow Settings")]                                     //VARIABLES FOR WHERE THE BOSS WILL SPAWN ITS SHADE
    public Vector2 shade1 = new Vector2(0, +2);                     //Where the boss will spawn its shade
    public Vector2 shade1Point;                                     //Shade area 1 position
    public Vector2 shade2 = new Vector2(0, -2);                     //Where the boss will spawn its shade
    public Vector2 shade2Point;                                     //Shade area 2 position
    public int hitCounterShade = 0;                                 //How much times the boss has been hit
    public int shadeCondition;                                      //How much time the boss is hit for the shades to spawn
    public int shadeKill = 2;                                       //How many shade you killed
    public int shade = 0;                                           //Variable for Shadow function
    public bool shootOff = false;                                   //Variable to turn shooting off
    //START FUNCTION
    void Start()
    {
        //INTIAL POINTS
        homePoint = boss.GetComponent<Transform>().position;
        startPoint = boss.GetComponent<Transform>().position;
        //SHADE POINTS
        shadeCondition = boss.GetComponent<BossHealth>().maxBossHealth / 4;
        shade1Point = new Vector2(startPoint.x + shade1.x, startPoint.y + shade1.y);
        shade2Point = new Vector2(startPoint.x + shade2.x, startPoint.y + shade2.y);
        //TELEPORT POINTS
        teleportArea1Point = new Vector2(startPoint.x + teleport1.x, startPoint.y + teleport1.y);
        teleportArea2Point = new Vector2(startPoint.x + teleport2.x, startPoint.y + teleport2.y);
    }
    //UPDATE FUNCTION
    void Update()
    {
        //SHADE SPAWN
        if (hitCounterShade > shadeCondition)
        {
            if (shade < 2)
            {
                Shadow();
                shade++;
            }
            else
                hitCounterShade = 0;
        }
        if (shadeKill == 2)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            shootOff = false;
            shade = 0;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            shootOff = true;
        }
        //TELEPORT CONDITION
        if (bossActive == true && hitCounter >= hitCounterTeleport)
        {
            Teleport();
            hitCounter = 0;
        }
        //UPGRADE CONDITION
        if (boss.GetComponent<BossHealth>().bossHealth < boss.GetComponent<BossHealth>().maxBossHealth / 4)
            hitCounterTeleport = 10;
        //WAVES
        if (bossActive == true)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer > firstWave && waveTimer < secondWave)
            {
                shootPatternDelayVar1 = 0.5f;
                shootPatternDelayVar2 = 1.25f;
                shootPatternDelayVar3 = 0.5f;
                if(shootOff == false)
                {
                    ShootPatternOneVar1(numberOfBullets);
                    ShootPatternOneVar2(numberOfBullets);
                }
            }
            else if (waveTimer > secondWave && waveTimer < thirdWave)
            {
                bulletSpeed = 3;
                if (shootOff == false)
                {
                    ShootPatternOneVar1(numberOfBullets);
                    ShootPatternOneVar2(numberOfBullets);
                    ShootPatternOneVar3(numberOfBullets);
                }
            }
            else if (waveTimer > thirdWave && waveTimer < fourthWave)
            {
                bulletSpeed = 5;
                if(shootOff == false)
                    ShootPatternTwoVar1();
            }
            else if (waveTimer > fourthWave && waveTimer < reset)
            {
                if(shootOff == false)
                    ShootPatternTwoVar2();
            }
            else
                waveTimer = 0;
        }
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hitCounter++;
            hitCounterShade++;
        }
    }
    //SHADOW FUNCTION
    void Shadow()
    {
        shadeKill = 0;
        if (shade == 0)
            Instantiate(bossShadow, shade1Point, Quaternion.identity);
        else if (shade == 1)
            Instantiate(bossShadow, shade2Point, Quaternion.identity);
    }
    //TELEPORT FUNCTION
    void Teleport()
    {
        if (home == true)
        {
            GetComponent<Transform>().position = teleportArea1Point;
            home = false;
            teleportArea1 = true;
        }
        else if (teleportArea1 == true)
        {
            boss.GetComponent<Transform>().position = teleportArea2Point;
            teleportArea1 = false;
            teleportArea2 = true;
        }
        else if (teleportArea2 == true)
        {
            boss.GetComponent<Transform>().position = homePoint;
            teleportArea2 = false;
            home = true;
        }
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
        if (shootPatternTimer1Var1 > shootPatternDelayVar1)
        {
            startPoint = boss.GetComponent<Transform>().position;
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
    protected void ShootPatternOneVar2(int numberOfBullets)
    {
        shootPatternTimer1Var2 += Time.deltaTime;
        if (shootPatternTimer1Var2 > shootPatternDelayVar2)
        {
            startPoint = boss.GetComponent<Transform>().position;
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
    protected void ShootPatternOneVar3(int numberOfBullets)
    {
        shootPatternTimer1Var3 += Time.deltaTime;
        if (shootPatternTimer1Var3 > shootPatternDelayVar3)
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
    //SHOOT PATTERN(TWO)(VARIATION ONE) FUNCTION
    void ShootPatternTwoVar1()
    {
        newTimer += Time.deltaTime;
        if (newTimer < 1f)
        {
            shootPatternDelayVar1 = 0.1f;
            ShootPatternOneVar1(numberOfBullets);
        }
        else if (newTimer > 1.15f && newTimer < 2.15f)
        {
            shootPatternDelayVar2 = 0.1f;
            ShootPatternOneVar2(numberOfBullets);
        }
        else if (newTimer > 2.3f)
            newTimer = 0f;
    }
    //SHOOT PATTERN(TWO)(VARIATION TWO) FUNCTION
    void ShootPatternTwoVar2()
    {
        newTimer += Time.deltaTime;
        if (newTimer < 1f)
        {
            shootPatternDelayVar1 = 0.1f;
            ShootPatternOneVar1(numberOfBullets);
        }
        else if (newTimer > 1.15f && newTimer < 2.15f)
        {
            shootPatternDelayVar2 = 0.1f;
            ShootPatternOneVar2(numberOfBullets);
        }
        else if (newTimer > 2.3f && newTimer < 3.3f)
        {
            shootPatternDelayVar3 = 0.1f;
            ShootPatternOneVar3(numberOfBullets);
        }
        else if (newTimer > 3.45f)
            newTimer = 0f;
    }
}
///END OF SCRIPT!
