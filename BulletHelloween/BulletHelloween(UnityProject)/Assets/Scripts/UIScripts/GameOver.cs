using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    //RETRY FUNCTION
    public void Retry()
    {
        PlayerPrefs.SetInt("lives", 3);
        SceneManager.LoadScene("Level 1");
    }
    //MAIN MENU FUNCTION
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    //QUIT FUNCTION
    public void Quit()
    {
        Application.Quit();
    }
}
///END OF SCRIPT!