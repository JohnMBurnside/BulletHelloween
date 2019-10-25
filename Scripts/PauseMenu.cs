using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    //VARIABLES
    public bool menuOn = false;
    //START FUNCTION
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
    //UPDATE FUNCTION
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && menuOn == false)
        {
            Time.timeScale = 0;
            menuOn = true;
            GetComponent<Canvas>().enabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && menuOn == true)
        {
            Time.timeScale = 1;
            menuOn = false;
            GetComponent<Canvas>().enabled = false;
        }
    }
    //MAIN MENU FUNCTION
    void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    //RESUME FUNCTION
    void Resume()
    {
        Time.timeScale = 1;
        menuOn = false;
        GetComponent<Canvas>().enabled = false;
    }
    //RESTART FUNCTION
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //QUIT FUNCTION
    void Quit()
    {
        Application.Quit();
    }
}
