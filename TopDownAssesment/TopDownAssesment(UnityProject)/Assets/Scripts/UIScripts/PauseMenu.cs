using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    //START FUNCTION
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
    //UPDATE FUNCTION
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1;
        }
    }
    //PAUSE FUNCTION(RESUME)
    public void Resume()
    {
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    //PAUSE FUNCTION(RESTART)
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //PAUSE FUNCTION(MAIN MENU)
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    //PAUSE FUNCTION(QUIT)
    public void Quit()
    {
        Application.Quit();
    }
}
