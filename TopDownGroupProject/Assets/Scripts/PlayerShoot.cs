using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    //VARIABLES
    //BALANCE
    public float shootDelay = 1.0f;
    public float bulletSpeed = 10.0f;
    public float bulletLifetime = 1.0f;
    public float timer = 0f;
    //BULLET
    public GameObject prefab; 
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
