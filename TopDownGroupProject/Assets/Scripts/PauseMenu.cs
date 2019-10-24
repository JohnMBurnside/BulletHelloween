using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (Time.timeScale == 1)
            {
                GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0;
            }
            else
            {
                GetComponent<Canvas>().enabled = false;
                Time.timeScale = 1;
            }
        
       
        }
     
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        GetComponent<Canvas>().enabled = false;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
