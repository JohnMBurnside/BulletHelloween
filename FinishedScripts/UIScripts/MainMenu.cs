using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //START GAME FUNCTION
    void StartGame()
    {
        PlayerPrefs.SetInt("lives", 3);
        SceneManager.LoadScene("Level 1");
    }
    //QUIT FUNCTION
    void Quit()
    {
        Application.Quit();
    }
}
