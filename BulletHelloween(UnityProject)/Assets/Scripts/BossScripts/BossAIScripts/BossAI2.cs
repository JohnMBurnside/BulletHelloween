using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossAI2 : MonoBehaviour
{
    /// <summary>
    /// ~Dracula Boss
    ///     -This will be the second boss and it'll be a lot harder than the first with a noticable amount of health diffrence
    ///     -Mechanics:
    ///         -The boss will teleport everytime the boss is hit 10 times
    ///     -Waves:
    ///         -This will have an intro wave where the boss shows off the teleport mechanic
    ///         -The first wave will be a simple shoot
    ///         -The second wave will be the first and second pattern
    ///         -The third and final wave will do the first, second and third pattern
    ///         -Repeat
    /// </summary>
    //VARIABLES	
    [Header("General Settings")]                                    //GENERAL VARIABLES
    public Transform player;                                        //Players transform location
    public GameObject prefab;                                       //Bullet prefab
    public GameObject boss;                                         //Boss object
    public Transform bossLocation;                                  //Bosses transform location
    public bool bossActive = false;                                 //Tells if the boss is active or not
    [Header("Base Stats Settings")]                                 //VARIABLES THAT'LL BALANCE THE BOSSES BASE STATS
    public float bulletSpeed = 5f;                                  //How fast the bullet will travel
    public float shootDelay = 0.5f;                                 //How long the boss will wait to fire again
    public float bulletLifetime = 10.0f;                            //How long the bullet will last before being destroyed
    float shootTimer = 0f;                                          //Timer for shooting  
    [Header("Wave Time Settings")]                                  //VARIABLES THAT'll CONTROL WHEN WAVES AND UPGRADES WILL OCCUR
    public int firstWave = 0;                                       //Time when the first wave will occur
    public int secondWave = 5;                                      //Time when the second wave will occur
    public int thirdWave = 15;                                      //Time when the third wave will occur
    public int reset = 25;                                          //Time when the waves will reset
    float waveTimer = 0f;                                           //Timer for the waves
    [Header("Shoot Pattern Settings")]                              //VARIABLES THAT'LL CONTROL SHOOTING PATTERNS
    public bool intro = false;                                      //Checks whether the boss did his intro attack or not
    public Vector2 startPoint;                                      //Variable for the starting position of the pattern
    public int numberOfBullets = 15;                                //How much bullets the enemy will shoot
    public float radius = 5f;                                       //Radius
    public float angle = 180f;                                      //Angle of bullet direction
    float shootPatternTimer1Var1 = 0f;                              //Timer for shoot pattern one variation one
    float shootPatternTimer1Var2 = 0f;                              //Timer for shoot pattern one variation two
    float shootPatternTimer1Var3 = 0f;                              //Timer for shoot pattern one variation three
    [Header("Teleport Settings")]                                   //TELEPORT VARIABLES
    [TextArea]                                                      //TELEPORT NOTES
    public string Notes =                                           //TELEPORT NOTES
    "TELEPORT NOTES\n" +                                            //TELEPORT NOTES
    "Most of these don't not have to be changed(only for testing)" +//TELEPORT NOTES
    "The only value that should change is 'teleportDelay'";         //TELEPORT NOTES
    public bool home = true;                                        //Checks whether the boss is in the home position
    public Vector2 homePoint;                                       //Home position
    public bool teleportArea1;                                      //Checks whether the boss is in the first teleport area position
    public Vector2 teleportArea1Point;                              //Teleport area 1 position
    public bool teleportArea2;                                      //Checks whether the boss is in the second teleport area position
    public Vector2 teleportArea2Point;                              //Teleport area 2 position
    public bool teleportArea3;                                      //Checks whether the boss is in the third teleport area position
    public Vector2 teleportArea3Point;                              //Teleport area 3 position
    public bool teleportArea4;                                      //Checks whether the boss is in the last teleport area position
    public Vector2 teleportArea4Point;                              //Teleport area 4 position
    public float teleportTimer;                                     //Teleport timer
    public float teleportDelay = 0.5f;                              //Teleport delay
    [Header("Teleport Condition Settings")]                         //VARIABLES THAT'll CONTROL WHEN WAVES AND UPGRADES WILL OCCUR
    public int hitCounter = 0;                                      //Measures how much times the enemy has been hit
    public int hitCounterTeleport = 25;                             //How much times the enemy will be hit before teleporting
    [Header("Teleport Direction Settings")]                         //VARIABLES FOR WHERE THE TELEPORTING WILL TAKE PLACE
    public Vector2 teleport1 = new Vector2(-5, +5);                 //How far the enemy will teleport the first time
    public Vector2 teleport2 = new Vector2(-5, -5);                 //How far the enemy will teleport the second time
    public Vector2 teleport3 = new Vector2(+5, -5);                 //How far the enemy will teleport the third time
    public Vector2 teleport4 = new Vector2(+5, +5);                 //How far the enemy will teleport the fourth time
    [Header("Animation Settings")]                                  //ANIMATION VARIABLES
    Animator draculaAC;                                             //Animator controller
    public bool teleportAnimation = false;                          //True or false variable to activate teleport animation
    public float animationTimer;                                    //Timer for animations
    public float animationSwitch = 2f;                              //How long until the animation will stop
    //START FUNCTION
    void Start()
    {
        draculaAC = GetComponent<Animator>();
        startPoint = boss.GetComponent<Transform>().position;
        homePoint = startPoint;
        teleportArea1Point = new Vector2(startPoint.x + teleport1.x, startPoint.y + teleport1.y);
        teleportArea2Point = new Vector2(startPoint.x + teleport2.x, startPoint.y + teleport2.y);
        teleportArea3Point = new Vector2(startPoint.x + teleport3.x, startPoint.y + teleport3.y);
        teleportArea4Point = new Vector2(startPoint.x + teleport4.x, startPoint.y + teleport4.y);
    }
    //UPDATE FUNCTION
    void Update()
    {
        startPoint = boss.GetComponent<Transform>().position;
        //INTRO
        if (bossActive == true && intro == false)
        {
            Teleport();
        }
        //WAVES
        if (bossActive == true && intro == true)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer > firstWave && waveTimer < secondWave)
                Shoot();
            else if (waveTimer > secondWave && waveTimer < thirdWave)
            {
                Shoot();
                ShootPatternOneVar1(numberOfBullets);
                ShootPatternOneVar2(numberOfBullets);
            }
            else if (waveTimer > thirdWave && waveTimer < reset)
            {
                Shoot();
                ShootPatternOneVar1(numberOfBullets);
                ShootPatternOneVar2(numberOfBullets);
                ShootPatternOneVar3(numberOfBullets);
            }
            else
                waveTimer = 0;
        }
        //TELEPORT CONDITION
        if (bossActive == true && intro == true && hitCounter >= hitCounterTeleport)
        {
            animationTimer += Time.deltaTime;
            teleportAnimation = true;
            if (animationTimer > animationSwitch)
            {
                teleportAnimation = false;
                Teleport();
                animationTimer = 0f;
                hitCounter = 0;
            }
        }
        //UPGRADE CONDITION
        if (boss.GetComponent<BossHealth>().bossHealth < boss.GetComponent<BossHealth>().maxBossHealth / 4)
            hitCounterTeleport = 5;
        //ANIMATION CONDITIONS
        if (teleportAnimation == true)
            GetComponent<Animator>().SetBool("teleportAnimation", true);
        if (teleportAnimation == false)
            GetComponent<Animator>().SetBool("teleportAnimation", false);
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && intro == true)
            hitCounter++;
    }
    //TELEPORT FUNCTION
    void Teleport()
    {
        if (intro == false)
        {
            teleportTimer += Time.deltaTime;
            if (teleportTimer > teleportDelay && home == true)
            {
                boss.GetComponent<Transform>().position = teleportArea1Point;
                Shoot();
                if (teleportTimer > teleportDelay * 2)
                {
                    home = false;
                    teleportArea1 = true;
                    teleportTimer = 0;
                }
            }
            else if (teleportTimer > teleportDelay && teleportArea1 == true)
            {
                boss.GetComponent<Transform>().position = teleportArea2Point;
                Shoot();
                if (teleportTimer > teleportDelay * 2)
                {
                    teleportArea1 = false;
                    teleportArea2 = true;
                    teleportTimer = 0;
                }
            }
            else if (teleportTimer > teleportDelay && teleportArea2 == true)
            {
                boss.GetComponent<Transform>().position = teleportArea3Point;
                Shoot();
                if (teleportTimer > teleportDelay * 2)
                {
                    teleportArea2 = false;
                    teleportArea3 = true;
                    teleportTimer = 0;
                }
            }
            else if (teleportTimer > teleportDelay && teleportArea3 == true)
            {
                boss.GetComponent<Transform>().position = teleportArea4Point;
                Shoot();
                if (teleportTimer > shootDelay * 2)
                {
                    teleportArea3 = false;
                    teleportArea4 = true;
                    teleportTimer = 0;
                }
            }
            else if (teleportTimer > teleportDelay && teleportArea4 == true)
            {
                boss.GetComponent<Transform>().position = homePoint;
                Shoot();
                if (teleportTimer > shootDelay * 2)
                {
                    teleportArea4 = false;
                    home = true;
                    intro = true;
                }
            }
        }
        else
        {
            if (home == true)
            {
                boss.GetComponent<Transform>().position = teleportArea1Point;
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
                boss.GetComponent<Transform>().position = teleportArea3Point;
                teleportArea2 = false;
                teleportArea3 = true;
            }
            else if (teleportArea3 == true)
            {
                boss.GetComponent<Transform>().position = teleportArea4Point;
                teleportArea3 = false;
                teleportArea4 = true;
            }
            else if (teleportArea4 == true)
            {
                boss.GetComponent<Transform>().position = homePoint;
                teleportArea4 = false;
                home = true;
            }
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
    protected void ShootPatternOneVar2(int numberOfBullets)
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
    protected void ShootPatternOneVar3(int numberOfBullets)
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