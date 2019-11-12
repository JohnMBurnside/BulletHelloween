using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    //VARIABLES                                         //VARIABLES
    [Header("General Settings")]                        //GENERAL VARIABLES
    public Transform player;                            //Player
    public GameObject prefab;                           //Bullet prefab
    [Header("Movement Settings")]                       //MOVEMENT BASED VARIABLES
    public Vector2 paceDirection;                       //Where the enemy will pace
    Vector3 startPosition;                              //The start position of the enemy
    public float chaseSpeed = 2.0f;                     //How fast the enemy will chase
    public float paceSpeed = 1.5f;                      //How fast the enemy will pace
    public float paceDistance = 3.0f;                   //How far the enemy will pace
    public float paceDuration = 3.0f;                   //How long the enemy will pace for before changing direction
    public float chaseTriggerDistance = 5.0f;           //The trigger distance for enemy chase
    public bool home = true;                            //Checks whether or not the enemy is home
    [Header("Shoot Settings")]                          //SHOOTING BASED VARIABLES
    public float bulletSpeed = 6.0f;                    //How fast the bullet will travel
    public float bulletLifetime = 1.0f;                 //How long the bullet will last for before being destroyed
    public float shootDelay = 0.5f;                     //How long the enemy will have to wait before shooting agian
    float timer = 0;                                    //Timer
    //START FUNCTION
    void Start()
    {
        startPosition = transform.position;
    }
    //UPDATE FUNCTION
    void Update()
    {
        Vector2 chaseDirection = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        if (chaseDirection.magnitude < chaseTriggerDistance)
        {
            Chase();
            Shoot();
        }
        else if (!home)
            GoHome();
        else
            Pace();
    }
    //CHASE FUNCTION
    void Chase()
    {
        home = false;
        Vector2 chaseDirection = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        chaseDirection.Normalize();
        GetComponent<Rigidbody2D>().velocity = chaseDirection * chaseSpeed;
    }
    //GO HOME FUNCTION
    void GoHome()
    {
        Vector2 homeDirection = new Vector2(startPosition.x - transform.position.x, startPosition.y - transform.position.y);
        if (homeDirection.magnitude < 0.2f)
        {
            transform.position = startPosition;
            home = true;
        }
        else
        {
            homeDirection.Normalize();
            GetComponent<Rigidbody2D>().velocity = homeDirection * paceSpeed;
        }
    }
    //PACE FUNCTION
    void Pace()
    {

        timer += Time.deltaTime;
        if (timer >= paceDuration)
        {
            paceDirection *= -1;
            timer = 0;
        }
        GetComponent<Rigidbody2D>().velocity = paceDirection * paceSpeed;
        ///ALT CODE
        /*Vector3 displacement = transform.position - startPosition;
        if (displacement.magnitude >= paceDistance)
        {
            paceDirection = -displacement;
            paceDirection.Normalize();
            GetComponent<Rigidbody2D>().velocity = paceDirection * paceSpeed;
        }*/
    }
    //SHOOT FUNCTION
    void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > shootDelay)
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
///END OF SCRIPT!