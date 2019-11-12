using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    //VARIABLES                                     //VARIABLES
    [Header("General Variables")]                   //GENERAL VARIABLES
    public GameObject prefab;                       //Prefab for bullet
    [Header("Shoot Variables")]                     //SHOOTING VARIABLES
    public float shootDelay = 1.0f;                 //How long the player will have to wait to shoot again
    public float bulletSpeed = 10.0f;               //How fast the bullet will travel
    public float bulletLifetime = 1.0f;             //How long the bullet will last before being destroyed
    public float timer = 0f;                        //Timer
    [Header("Shoot Variable Upgrades")]             //UPGRADED SHOOTNG VARIABLES
    public float shootDelayUpgrade = 0.2f;          //How long the player will have to wait to shoot again(post upgrade)
    public float bulletSpeedUpgrade = 12f;          //How fast the bullet will travel(post upgrade)
    public float bulletLifetimeUpgrade = 2.0f;      //How long the bullet will last before being destroyed(post upgrade)
    //UPDATE FUNCTION
    void Update()
    {
        Shoot();
    }
    //SHOOT FUNCTION
    public void Shoot()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer > shootDelay)
        {
            timer = 0;
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 shootDir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            shootDir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
}
///END OF SCRIPT!