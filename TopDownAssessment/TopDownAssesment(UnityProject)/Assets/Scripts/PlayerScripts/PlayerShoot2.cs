using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShoot2 : MonoBehaviour
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
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer > shootDelay)
        {
            timer = 0;
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToViewportPoint(mousePosition);
            Vector2 shootDir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            shootDir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
    }
}