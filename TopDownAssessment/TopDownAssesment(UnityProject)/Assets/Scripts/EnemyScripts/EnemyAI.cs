using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    //VARIABLES
    public Transform player;
    public GameObject prefab;
    public float chaseSpeed = 2.0f;
    public float paceSpeed = 1.5f;
    public float paceDistance = 3.0f;
    public float chaseTriggerDistance = 5.0f;
    public float shootDelay = 0.0f;
    public float bulletSpeed = 10.0f;
    public float bulletLifetime = 1.0f;
    public float timer = 0.0f;
    public Vector2 paceDirection;
    public Vector3 startPosition;
    public bool home = true;
    //START FUNCTION
    void Start()
    {
        startPosition = transform.position;
    }
    //UPDATE FUNCTION
    void Update()
    {
        Shoot();
        timer += Time.deltaTime;
        Vector2 chaseDirection = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        if (chaseDirection.magnitude < chaseTriggerDistance)
        {
            Chase();
        }
        else if (!home)
        {
            GoHome();
        }
        else
        {
            Pace();
        }
    }
    //CHASE FUNCTION
    void Chase()
    {
        home = false;
        Vector2 chaseDirection = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        chaseDirection.Normalize();
        //transform.up = chaseDirection;
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
            //transform.up = homeDirection;
            GetComponent<Rigidbody2D>().velocity = homeDirection * paceSpeed;
        }
    }
    //PACE FUNCTION
    void Pace()
    {
        Vector3 displacement = transform.position - startPosition;
        if (displacement.magnitude >= paceDistance)
            paceDirection = -displacement;
        paceDirection.Normalize();
        //transform.up = paceDirection;
        GetComponent<Rigidbody2D>().velocity = paceDirection * paceSpeed;
    }
    //SHOOT FUNCTION
    void Shoot()
    {
        if (timer > shootDelay)
        {
            timer = 0;
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            Vector2 shootDir = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
            shootDir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
}