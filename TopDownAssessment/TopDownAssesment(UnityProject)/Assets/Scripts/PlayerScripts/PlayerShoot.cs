using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    //VARIABLES
    public GameObject prefab;
    public float bulletSpeed = 10.0f;
    public float bulletLifetime = 1.0f;
    public float shootDelay = 1.0f;
    float timer = 0;
    //UPDATE FUNCTION
    void Update()
    {
        shoot();
    }
    //SHOOT FUNCTION
    public void shoot()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer > shootDelay)
        {
            timer = 0;
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            Vector3 mousePostion = Input.mousePosition;
            mousePostion = Camera.main.ScreenToViewportPoint(mousePostion);
            Vector2 shootDir = new Vector2(mousePostion.x - transform.position.x, mousePostion.y - transform.position.y);
            shootDir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
}
