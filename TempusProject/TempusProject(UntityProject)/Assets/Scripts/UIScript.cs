using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Tempus;
public class UIScript : MonoBehaviour
{
    #region VARAIBLES
    [Header("General Settings")]
    public UIEnum UI;
    //[Header("Main Menu UI Settings")]
    //[Header("Pause UI Settings")]
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        switch(UI)
        {
            case UIEnum.Main:
                break;
            case UIEnum.Pause:
                break;
        }
    }
    #endregion
    //UI FUNCTIONS
    #region START GAME FUNCTION
    public void StartGame()
    {
        //Start game
    }
    #endregion
    #region QUIT FUNCTION
    public void QUIT()
    {
        Application.Quit();
    }
    #endregion
    #region RESUME FUNCTION
    public void Resume()
    {
        //Resume game
    }
    #endregion
    #region QUIT TO MENU FUNCTION
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
    #region SHOW TEXT FUNCTION
    public IEnumerator ShowText(float delay, string fullText)
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            string currentText = "";
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
            //Calling the function
            //StartCoroutine(ShowText());
        }
    }
    #endregion
}
