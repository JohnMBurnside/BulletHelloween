using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public int lives;
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetInt("lives", lives); 
    }
}
