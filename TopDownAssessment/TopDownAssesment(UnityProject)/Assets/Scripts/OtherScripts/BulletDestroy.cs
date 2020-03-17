using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletDestroy : MonoBehaviour
{
    //DESTROY FUNCTION
    void OnColliderEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}