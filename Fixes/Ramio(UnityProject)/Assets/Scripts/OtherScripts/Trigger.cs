#region NAMESPACES
using UnityEngine;
#endregion
public class Trigger : MonoBehaviour
{
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerCollision>().bossStart = true;
    }
    #endregion
}
