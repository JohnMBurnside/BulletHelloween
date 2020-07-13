#region NAMESPACES
using UnityEngine;
#endregion
public class Wall : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    public bool destroyOnStart = true;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        if(destroyOnStart == true)
            Destroy(this);
    }
    #endregion
    #region ON DRAW GIZMOS FUNCTION
    void OnDrawGizmosSelected(){Gizmos.DrawCube(transform.position, transform.localScale);}
    #endregion
}
