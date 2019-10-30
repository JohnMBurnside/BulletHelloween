using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletDestroy : MonoBehaviour
{
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
