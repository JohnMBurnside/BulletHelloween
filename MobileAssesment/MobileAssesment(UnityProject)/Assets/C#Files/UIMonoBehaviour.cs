#region NAMESPACES
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion
public enum UICanvasType { MainMenu, PauseMenu }
public class UIMonoBehaviour : MonoBehaviour
{
    #region VARIABLES
    public UICanvasType UI;
    public Animator fadeAnimator;
    float timer;
    bool pauseOn;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        if (UI == UICanvasType.PauseMenu)
            GetComponent<Canvas>().enabled = false;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
            fadeAnimator.gameObject.transform.parent.gameObject.SetActive(false);
    }
    #endregion
    //UI FUNCTIONS
    #region START GAME FUNCTION
    public void StartGame() { StartCoroutine(StartGameC()); }
    #endregion
    #region START GAME C FUNCTION
    IEnumerator StartGameC()
    {
        timer = 0;
        fadeAnimator.gameObject.transform.parent.gameObject.SetActive(true);
        fadeAnimator.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level 1");
    }
    #endregion
    #region PASUE FUNCTION
    public void Pause()
    {
        if (pauseOn == false)
        {
            GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
            pauseOn = true;
        }
        else if (pauseOn == true)
        {
            GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1;
            pauseOn = false;
        }
    }
    #endregion
    #region RESTART FUNCTION
    public void Restart()
    {
        Time.timeScale = 1;
        string name = SceneManager.GetActiveScene().name;
        if (name == "Level 1")
            SceneManager.LoadScene("Level 1");
        else if (name == "Level 2")
            SceneManager.LoadScene("Level 2");
    }
    #endregion
    #region RESTART C FUNCTION
    IEnumerator RestartC()
    {
        timer = 0;
        fadeAnimator.gameObject.transform.parent.gameObject.SetActive(true);
        fadeAnimator.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1f);
        Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
    #region MAIN MENU FUNCTION
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
    #region MAIN MENU C FUNCTION
    IEnumerator MainMenuC()
    {
        timer = 0;
        fadeAnimator.gameObject.transform.parent.gameObject.SetActive(true);
        fadeAnimator.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1f);
        Pause();
        SceneManager.LoadScene("Main Menu");
    }
    #endregion
    #region QUIT FUNCTION
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
    #region QUIT C FUNCTION
    IEnumerator QuitC()
    {
        timer = 0;
        fadeAnimator.gameObject.transform.parent.gameObject.SetActive(true);
        fadeAnimator.SetBool("FadeOut", true);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
    #endregion
}
