#region NAMESPACES
using UnityEngine;
#endregion
public class EndingUI : MonoBehaviour
{
    //UNITY FUNCTIONS
    #region UPDATE FUNCTION
    void Update(){GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * 100;}
    #endregion
}
