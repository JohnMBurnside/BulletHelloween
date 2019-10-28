using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    //VARIABLES
    //GENERAL VARIABLES
    public Transform player;
    //MOVEMENT VARIABLES
    public Vector2 paceDirection;
    Vector3 startPosition;
    public float chaseSpeed = 2.0f;
    public float paceSpeed = 1.5f;
    public float paceDistance = 3.0f;
    public float paceDuration = 3.0f;
    public float chaseTriggerDistance = 5.0f;
    public bool home = true;
    //SHOOT VARIABLES
    public GameObject prefab;
    public float bulletSpeed = 6.0f;
    public float bulletLifetime = 1.0f;
    public float shootDelay = 0.5f;
    float timer = 0;
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
            Debug.Log(playerPosition);
            Vector2 shootDir = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
            shootDir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
}