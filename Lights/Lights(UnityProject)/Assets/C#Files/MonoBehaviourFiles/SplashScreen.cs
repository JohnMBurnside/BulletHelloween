#region NAMESPACES
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion
public class SplashScreen : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    float splashScreenTimer;
    #endregion
    //UNITY FUNCTIONS
    #region UPDATE FUNCTION
    void Update()
    {
        splashScreenTimer += Time.deltaTime;
        if (splashScreenTimer > 4f)
            SceneManager.LoadScene("Main Menu");
    }
    #endregion
}
