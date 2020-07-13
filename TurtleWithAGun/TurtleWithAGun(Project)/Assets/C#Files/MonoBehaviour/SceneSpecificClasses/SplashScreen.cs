using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreen : MonoBehaviour
{
    #region START FUNCTION
    void Start()
        { StartCoroutine(Splash()); }
    #endregion
    #region SPLASH FUNCTION
    IEnumerator Splash()
    {
        Cursor.visible = false;
        yield return new WaitForSeconds(4);
        PlayerData data = SaveSystem.Load();
        if (data == null)
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
