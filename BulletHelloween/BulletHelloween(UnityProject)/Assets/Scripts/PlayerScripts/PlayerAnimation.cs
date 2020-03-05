#region NAMESPACES
using UnityEngine;
#endregion
public class PlayerAnimation : MonoBehaviour
{
    //UNITY FUNCTIONS
    #region UPDATE FUNCTION
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        GetComponent<Animator>().SetFloat("x", x);
        GetComponent<Animator>().SetFloat("y", y);
    }
    #endregion
#if UNITY_ANDROID
    #region VARIABLES
    public Joystick joystick;
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        GetComponent<Animator>().SetFloat("x", x);
        GetComponent<Animator>().SetFloat("y", y);
    }
    #endregion
#endif
}