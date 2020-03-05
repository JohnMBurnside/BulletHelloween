#region NAMESPACES
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion
public enum UIOptions { MainMenu, PauseMenu, Fade, Shop, ShopPrompt }
public class UIScript : MonoBehaviour
{
    #region VARIABLES
    [Header("UI Settings")]
    public UIOptions UI;
    bool pauseOn;
    bool loreOn;
    [Header("Animation Settings")]
    public Animator animator;
    GameObject player;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        if (UI == UIOptions.PauseMenu)
            GetComponent<Canvas>().enabled = false;
        if(UI == UIOptions.Fade)
            player = GameObject.Find("Player");
        if (UI == UIOptions.ShopPrompt)
        {
            player = GameObject.Find("Player");
            GetComponentInParent<Canvas>().enabled = false;
        }
        try { GameObject.Find("UI/Lore").GetComponent<Canvas>().enabled = false; }
        catch { }
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //Pausing
        if (Input.GetKeyDown(KeyCode.Escape) && pauseOn == false && UI == UIOptions.PauseMenu)
        {
            GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
            pauseOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseOn == true && UI == UIOptions.PauseMenu)
        {
            GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1;
            pauseOn = false;
        }
    }
    #endregion
    //UI FUNCTIONS
    #region START GAME FUNCTION
    public void StartGame()
    {
        PlayerPrefs.SetInt("lives", 3);
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetInt("coins", 0);
        animator.SetTrigger("FadeOut");
    }
    #endregion
    #region LORE FUNCTION
    public void Lore()
    {
        loreOn = !loreOn;
        if (loreOn == true)
        {
            GameObject.Find("UI/MainMenu").GetComponent<Canvas>().enabled = false;
            GameObject.Find("UI/Lore").GetComponent<Canvas>().enabled = true;
        }
        else
        {
            GameObject.Find("UI/MainMenu").GetComponent<Canvas>().enabled = true;
            GameObject.Find("UI/Lore").GetComponent<Canvas>().enabled = false;
        }
    }
    #endregion
    #region EXIT GAME FUNCTION
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
    #region RESUME FUNCTION
    public void Resume()
    {
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    #endregion
    #region RESTART FUNCTION
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    #endregion
    #region MAIN MENU FUNCTION
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    #endregion
    #region ON FADE COMPLETE FUNCTION
    public void OnFadeComplete() 
    {
        if (UI == UIOptions.MainMenu)
            SceneManager.LoadScene("Tutorial");
        else if (UI == UIOptions.Fade)
            SceneManager.LoadScene("Level " + player.GetComponent<PlayerCollision>().level);
    }
    #endregion
}
