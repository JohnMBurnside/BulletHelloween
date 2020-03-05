#region NAMESPACES
using UnityEngine;
#endregion
public class LevelThreeMechanic : MonoBehaviour
{
    #region VARIABLES
    [Header("Mechanic Settings")]
    public GameObject ceiling;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start(){ceiling.tag = "Smashed";}
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ceiling.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    #endregion
}
