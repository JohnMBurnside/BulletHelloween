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
        if (Input.GetKeyDown(KeyCode.Escape) && menuOn == false)
        {
            Time.timeScale = 0;
            menuOn = true;
            GetComponent<Canvas>().enabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && menuOn == true)
        {
            Time.timeScale = 1;
            menuOn = false;
            GetComponent<Canvas>().enabled = false;
        }
    }
    //MAIN MENU FUNCTION
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
    //RESUME FUNCTION
    public void Resume()
    {
        Time.timeScale = 1;
        menuOn = false;
        GetComponent<Canvas>().enabled = false;
    }
    //RESTART FUNCTION
   public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //QUIT FUNCTION
   public void Quit()
    {
        Application.Quit();
    }
}
///END OF SCRIPT!