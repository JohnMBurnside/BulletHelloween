using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletDestroy : MonoBehaviour
{

    //VARIABLES                                 //VARIABLES
    [Header("General Settings")]                //GENERAL VARIABLES
    public int speedRotate = 50;                //How fast the bullet will spin
    //UPDATE FUNCTION
    void Update()
    {
        transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
    }
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
///END OF SCRIPT!
