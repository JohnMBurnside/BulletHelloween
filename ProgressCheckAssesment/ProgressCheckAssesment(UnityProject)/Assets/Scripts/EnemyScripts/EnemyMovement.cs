using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyPace : MonoBehaviour
{
    //VARIABLES
    float timer = 0;
    public Vector2 moveDir;
    public float moveSpeed = 3.0f;
    public float paceDuration = 3.0f;
    //START FUNCTION
    void Start()
    {
        moveDir.Normalize();
    }
    //UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= paceDuration)
        {
            moveDir *= -1;
            timer = 0;
        }
        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;
    }
}
