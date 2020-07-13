using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum UIs
{
    None = 0,
    MainMenu = 1,
    PauseMenu = 2,
    PlayerHUD= 3,
    Fade = 4
};
public class UI : MonoBehaviour
{
    #region VARIABLES
    public UIs UIType = UIs.MainMenu;
    [Header("General Settings")]
    public Player player;
    [Header("Main Menu or Pause Menu Settings")]
    public Canvas optionsCanvas;
    public Canvas creditsCanvas;
    [Header("Option Settings")]
    public Text fullscreenText;
    public Text framerateText;
    public Text areYouSureText;
    [Header("Credits Settings")]
    public GameObject creditsRoll;
    public float speed;
    float timer;
    Vector3 creditsPos;
    [Header("Player HUD Settings")]
    public Slider healthSlider;
    public Image bloodFade;
    public GameObject[] bulletInventory;
    public GameObject map;
    GameObject[] mapParts;
    [Header("Animation Settings")]
    public Animator animator;
    #endregion
    #region START FUNCTION
    void Start()
    {
        switch (UIType)
        {
            case UIs.MainMenu:
                MainAndPauseSetup();
                Cursor.visible = true;
                print(gameObject);
                creditsPos = creditsRoll.GetComponent<RectTransform>().position;
                GetComponent<Canvas>().enabled = true;
                creditsCanvas.enabled = false;
                Screen.fullScreen = BoolPrefs.GetBool("fullscreen");
                areYouSureText.gameObject.SetActive(false);
                break;
            case UIs.PauseMenu:
                MainAndPauseSetup();
                GetComponent<Canvas>().enabled = false;
                break;
            case UIs.PlayerHUD:
                healthSlider.maxValue = player.maxHealth;
                mapParts = new GameObject[map.transform.childCount];
                for (int i = 0; i < map.transform.childCount; i++)
                    mapParts[i] = map.transform.GetChild(i).gameObject;
                break;
        }
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        switch (UIType)
        {
            case UIs.MainMenu:
                if (creditsCanvas.enabled)
                {
                    if (timer < 12.5f)
                    {
                        timer += Time.deltaTime;
                        creditsRoll.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
                    }
                    else
                        Credits();
                }
                break;
            case UIs.PauseMenu:
                if (Input.GetKeyDown(KeyCode.Escape) && GetComponent<Canvas>().enabled == false && optionsCanvas.enabled == false)
                {
                    Time.timeScale = 0;
                    GetComponent<Canvas>().enabled = true;
                    Cursor.visible = true;
                }
                else if (Input.GetKeyDown(KeyCode.Escape) && GetComponent<Canvas>().enabled)
                {
                    Time.timeScale = 1;
                    GetComponent<Canvas>().enabled = false;
                    Cursor.visible = false;
                    optionsCanvas.enabled = false;
                }
                break;
            case UIs.PlayerHUD:
                if (Input.GetKey(KeyCode.Tab))
                    Map(player.chapter);
                else
                    map.SetActive(false);
                healthSlider.value = player.currentHealth;
                float healthPercent = 1 - (player.currentHealth / player.maxHealth);
                if(healthPercent > .5f)
                    bloodFade.color = new Color(bloodFade.color.r, bloodFade.color.g, bloodFade.color.b, healthPercent / 2);
                else if(healthPercent < .5f)
                    bloodFade.color = new Color(bloodFade.color.r, bloodFade.color.g, bloodFade.color.b, 0);
                break;
        }
    }
    #endregion
    #region MAIN AND PAUSE SETUP FUNCTION
    void MainAndPauseSetup()
    {
        optionsCanvas.enabled = false;
        if (Screen.fullScreen)
            fullscreenText.text = "Fullscreen: On";
        else
            fullscreenText.text = "Fullscreen: Off";
        int framerate = PlayerPrefs.GetInt("framerate");
        if (framerate <= 1)
        {
            PlayerPrefs.SetInt("framerate", 1);
            QualitySettings.vSyncCount = 1;
            framerateText.text = "FrameRate: VSync";
        }
        else
        {
            Application.targetFrameRate = framerate;
            framerateText.text = "FrameRate: " + framerate;
        }
    }
    #endregion
    #region CONTINUE FUNCTION
    public void Continue()
    {
        PlayerData data = SaveSystem.Load();
        Cursor.visible = false;
        StartCoroutine(FadeOut(data.area, false, PlayerPrefs.GetInt("triggerIndex")));
    }
    #endregion
    #region NEW GAME FUNCTION
    public void NewGame()
    {
        PlayerPrefs.SetInt("triggerIndex", 0);
        Cursor.visible = false;
        StartCoroutine(FadeOut("Tutorial", false, PlayerPrefs.GetInt("triggerIndex"))); 
    }
    #endregion
    #region OPTIONS FUNCTION
    public void Options()
    {
        if (!optionsCanvas.enabled)
        {
            GetComponent<Canvas>().enabled = false;
            optionsCanvas.enabled = true;
        }
        else
        {
            optionsCanvas.enabled = false;
            GetComponent<Canvas>().enabled = true;
        }
    }
    #endregion
    #region CREDITS FUNCTION
    public void Credits()
    {
        if (!creditsCanvas.enabled)
        {
            GetComponent<Canvas>().enabled = false;
            creditsCanvas.enabled = true;
            timer = 0;
            creditsRoll.GetComponent<RectTransform>().position = creditsPos;
        }
        else
        {
            creditsCanvas.enabled = false;
            GetComponent<Canvas>().enabled = true;
        }
    }
    #endregion
    #region QUIT GAME
    public void QuitGame()
    {
        if (UIType == UIs.MainMenu)
            Application.Quit();
        else if (UIType == UIs.PauseMenu)
        {
            SaveSystem.Save(player);
            Application.Quit();
        }
    }
    #endregion
    #region FULLSCREEN FUNCTION
    public void Fullscreen()
    {
        if (Screen.fullScreen == true)
        {
            Screen.fullScreen = false;
            BoolPrefs.SetBool("fullscreen", false);
            fullscreenText.text = "Fullscreen: Off";
        }
        else
        {
            Screen.fullScreen = true;
            BoolPrefs.SetBool("fullscreen", true);
            fullscreenText.text = "Fullscreen: On";
        }
    }
    #endregion
    #region FRAMERATE FUNCTION
    public void FrameRate()
    {
        switch (PlayerPrefs.GetInt("framerate"))
        {
            case 1:
                PlayerPrefs.SetInt("framerate", 30);
                QualitySettings.vSyncCount--;
                Application.targetFrameRate = 30;
                framerateText.text = "FrameRate: 30";
                break;
            case 30:
                PlayerPrefs.SetInt("framerate", 60);
                Application.targetFrameRate = 60;
                framerateText.text = "FrameRate: 60";
                break;
            case 60:
                PlayerPrefs.SetInt("framerate", 90);
                Application.targetFrameRate = 90;
                framerateText.text = "FrameRate: 90";
                break;
            case 90:
                PlayerPrefs.SetInt("framerate", 120);
                Application.targetFrameRate = 120;
                framerateText.text = "FrameRate: 120";
                break;
            case 120:
                PlayerPrefs.SetInt("framerate", 144);
                Application.targetFrameRate = 144;
                framerateText.text = "FrameRate: 144";
                break;
            case 144:
                PlayerPrefs.SetInt("framerate", 240);
                Application.targetFrameRate = 240;
                framerateText.text = "FrameRate: 240";
                break;
            case 240:
                PlayerPrefs.SetInt("framerate", 300);
                Application.targetFrameRate = 300;
                framerateText.text = "FrameRate: 300";
                break;
            case 300:
                PlayerPrefs.SetInt("framerate", 1);
                Application.targetFrameRate = 60;
                QualitySettings.vSyncCount++;
                framerateText.text = "FrameRate: VSync";
                break;
        }
    }
    #endregion
    #region DELETE SAVE DATA FUNCTION
    public void DeleteSaveData(int button)
    {
        if (button == 0)
            areYouSureText.gameObject.SetActive(true);
        else if (button == 1)
            areYouSureText.gameObject.SetActive(false);
        else if (button == 2)
        {
            SaveSystem.Delete();
            PlayerPrefs.SetInt("triggerIndex", 0);
            Cursor.visible = false;
            StartCoroutine(FadeOut("SplashScreen", false, PlayerPrefs.GetInt("triggerIndex")));
        }    
    }
    #endregion
    #region RESUME FUNCTION
    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        GetComponent<Canvas>().enabled = false;
        optionsCanvas.enabled = false;
    }
    #endregion
    #region SAVE GAME FUNCTION
    public void SaveGame()
        { SaveSystem.Save(player); }
    #endregion
    #region MAIN MENU FUNCTION
    public void MainMenu()
    {
        Time.timeScale = 1;
        SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
    #region UNLOCK BULLETS FUNCTION
    public void UnlockBullets(int bulletsUnlocked)
    {
        for (int i = 0; i < bulletsUnlocked; i++)
            bulletInventory[i].SetActive(true);
    }
    #endregion
    #region MAP FUNCTION
    void Map(int chapter)
    {
        map.SetActive(true);
        for (int i = 1; i < chapter; i++) 
            mapParts[i].SetActive(true);
    }
    #endregion
    #region FADE OUT FUNCTION
    public IEnumerator FadeOut(string areaToLoad, bool save, int triggerIndex)
    {
        PlayerPrefs.SetInt("triggerIndex", triggerIndex);
        if (areaToLoad == "GameOver")
            yield return new WaitForSeconds(1.5f);
        animator.SetBool("fadeOut", true);
        if(save)
        {
            if (player == null)
            {
                try
                {
                    GameObject.Find("Bob");
                    SaveSystem.Save(player);
                }
                catch
                    { Debug.LogError("Player cannot save because player was not set"); }
            }
            else
                SaveSystem.Save(player);
        }
        yield return new WaitForSeconds(1.15f);
        SceneManager.LoadScene(areaToLoad);
    }
    #endregion
}
