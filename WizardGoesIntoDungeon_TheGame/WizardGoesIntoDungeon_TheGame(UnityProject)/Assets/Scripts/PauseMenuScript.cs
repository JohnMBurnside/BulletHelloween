using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    bool menuOn;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && menuOn == false)
        {
            Time.timeScale = 0;
            GetComponent<Canvas>().enabled = true;
            menuOn = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && menuOn == true)
        {
            Time.timeScale = 1;
            GetComponent<Canvas>().enabled = false;
            menuOn = false;
        }
    }
    #endregion
    //PAUSE MENU FUNCTIONS
    #region CONTINUE FUNCTION
    public void Continue()
    {
        Time.timeScale = 1;
        GetComponent<Canvas>().enabled = false;
        menuOn = false;
    }
    #endregion
    #region RESTART FUNCTION
    public void Restart()
    {
        Time.timeScale = 1;
        GetComponent<Canvas>().enabled = false;
        menuOn = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
    #region MAIN MENU FUNCTION
    public void MainMenu()
    {
        Time.timeScale = 1;
        GetComponent<Canvas>().enabled = false;
        menuOn = false;
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
