#region NAMESPACES
using UnityEngine;
#endregion
public class CameraMonoBehavoir : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    GameObject player;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        player = GameObject.Find("Player");
        if (transform.parent != null)
            transform.parent = null;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update() { GetComponent<Transform>().position = new Vector3(player.transform.position.x, player.transform.position.y, -10); }
    #endregion
}
