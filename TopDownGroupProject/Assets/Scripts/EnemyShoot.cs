using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject prefab;
    public Transform player;
    public float bulletSpeed = 6.0f;
    public float bulletLifetime = 1.0f;
    public float shootDelay = 0.5f;
    float timer = 0;



    // Start is called before the first frame update
    void Start()
    {
        //player.position.x;
        //player.position y;

    }

    // Update is called once per frame
    void Update()
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

