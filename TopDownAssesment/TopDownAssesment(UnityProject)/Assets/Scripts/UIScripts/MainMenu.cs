using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //START FUNCTION
    void Start()
    {
        GetComponent<Canvas>().enabled = true;
    }
    //FUNCTION (START GAME)
    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }
    //FUNCTION (QUIT)
    public void Quit()
    {
        Application.Quit();
    }
}
