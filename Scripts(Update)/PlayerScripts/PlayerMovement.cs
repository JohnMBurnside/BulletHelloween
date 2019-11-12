using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    //VARIABLES                             //VARIABLES
    [Header("General Variables")]           //GENERAL SETTINGS
    public float speed = 5.0f;              //How fast you want the player to travel
    //UPDATE FUNCTION
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 moveDir = new Vector2(x, y);
        GetComponent<Rigidbody2D>().velocity = moveDir * speed;
    }
}
///END OF SCRIPT!