#region NAMESPACES
using UnityEngine;
#endregion
public class PlayerMovement : MonoBehaviour
{
#if UNITY_PC
    //VARIABLES
    public float speed = 5.0f;
    //UPDATE FUNCTION
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 moveDir = new Vector2(x, y);
        GetComponent<Rigidbody2D>().velocity = moveDir * speed;
    }
#endif
#if UNITY_ANDROID
    #region VARIABLES
    public Joystick joystick;
    public float speed = 5.0f;
    #endregion
    //UNITY FUNCTIONS
    #region UPDATE FUNCTION
    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        Vector2 moveDir = new Vector2(x, y);
        GetComponent<Rigidbody2D>().velocity = moveDir * speed;
    }
    #endregion
#endif
}