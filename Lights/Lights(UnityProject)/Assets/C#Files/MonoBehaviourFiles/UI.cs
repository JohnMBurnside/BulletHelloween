#region NAMESPACES
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
#endregion
public class UI : MonoBehaviour
{
    #region VARIABLES
    [Header("General Variables")]
    public UITypes UIType;
    [Header("Main Menu Settings")]
    public GameObject[] mainMenuSections; //Main Menu Section objects must be in this order: Main, Main(WithSaveData), Level Select, Options
    public VersionControlObject version;
    [HideInInspector] public GameObject mainMenuLightingObject;
    [HideInInspector] public bool continueGame;
    [HideInInspector] public bool newGame;
    [HideInInspector] public bool quitGame;
    [HideInInspector] public bool levelButton;
    [HideInInspector] public int levelSelectNumber;
    [Header("Pause Settings")]
    bool pauseOn;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        //Main Menu
        if(UIType == UITypes.MainMenu)
        {
            //Load save data for level
            int level = SaveSystem.Load().level;
            //Get the Main Menu Section objects
            for (int i = 0; i < mainMenuSections.Length; i++)
                mainMenuSections[i] = transform.GetChild(i).gameObject;
            //Turn all the Main Menu Secions objects off
            foreach (GameObject objects in mainMenuSections)
                objects.SetActive(false);
            //Turn on the Main Menu depeding on whether you have save data in the game
            if (level > 0)
                mainMenuSections[1].SetActive(true);
            else
                mainMenuSections[0].SetActive(true);
            //Get level select object
            GameObject levelSelectObject = GameObject.Find("UI/MainMenu/LevelSelect/Levels");
            //Set all objects to false
            for (int i = 0; i < levelSelectObject.transform.childCount; i++)
                levelSelectObject.transform.GetChild(i).gameObject.SetActive(false);
            //Set objects only the levels that the player has progressed to on
            for (int i = 0; i < (level); i++)
                levelSelectObject.transform.GetChild(i).gameObject.SetActive(true);
            //Find all the text components under the Main Menu canvas
            Text[] mainMenuText = GetComponentsInChildren<Text>();
            //Within the Main Menu Text array find the version object and set the text to the current version
            foreach(Text text in mainMenuText)
                if (text.gameObject.name == "Version")
                    text.text = version.versionPhase + "/" + version.version;
            //Find the Menu Lighting object
            mainMenuLightingObject = GameObject.Find("MenuLighting");
        }
        //Pausing
        if (UIType == UITypes.PauseMenu)
            GetComponent<Canvas>().enabled = false;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //Pausing
        if(UIType == UITypes.PauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }
    }
    #endregion
    //UI FUNCTIONS
    #region CONTINUE FUNCTION
    public void Continue()
    {
        continueGame = true;
        mainMenuLightingObject.GetComponent<MenuLighting>().ExitMainMenu();
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
            button.interactable = false;
    }
    #endregion
    #region NEW GAME FUNCTION
    public void NewGame()
    {
        newGame = true;
        mainMenuLightingObject.GetComponent<MenuLighting>().ExitMainMenu();
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
            button.interactable = false;
    }
    #endregion
    #region LEVEL SELECT FUNCTION
    public void LevelSelect()
    {

    }
    #endregion
    #region LEVEL NUMBER FUNCTION
    public void LevelButton(int levelIndex)
    {
        levelSelectNumber = levelIndex;
        levelButton = true;
        mainMenuLightingObject.GetComponent<MenuLighting>().ExitMainMenu();
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
            button.interactable = false;
    }
    #endregion
    #region OPTIONS FUNCTION
    public void Options()
    {
        if(UIType == UITypes.MainMenu)
        {

        }
        if(UIType == UITypes.PauseMenu)
        {

        }
    }
    #endregion
    #region PAUSE FUNCTION
    public void Pause()
    {

        if (pauseOn == false)
        {
            Time.timeScale = 0;
            GetComponent<Canvas>().enabled = true;
            pauseOn = true;
        }
        else if (pauseOn == true)
        {
            Time.timeScale = 1;
            GetComponent<Canvas>().enabled = false;
            pauseOn = false;
        }
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
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
    #region MAIN MENU FUNCTION
    public void MainMenu()
    {
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
    #endregion
    #region QUIT GAME FUNCTION
    public void QuitGame() 
    { 
        if(UIType == UITypes.MainMenu)
        {
            quitGame = true;
            mainMenuLightingObject.GetComponent<MenuLighting>().ExitMainMenu();
            Button[] buttons = GetComponentsInChildren<Button>();
            foreach (Button button in buttons)
                button.interactable = false;
        }
        else
            Application.Quit();
    }
    #endregion
}
[CustomEditor(typeof(UI))]
public class UIEditor : Editor
{
    #region VARIABLES
    UI script;
    #endregion
    //UNITY FUNCTIONS
    #region ON INSPECTOR GUI FUNCTION
    public override void OnInspectorGUI()
    {
        script = (UI)target;
        script.UIType = (UITypes)EditorGUILayout.EnumPopup("UI", script.UIType);
        //Main Menu
        if (script.UIType == UITypes.MainMenu)
        {
            //Main Menu Sections array
            serializedObject.Update();
            SerializedProperty tps = serializedObject.FindProperty("mainMenuSections");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(tps, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
            //Version text
            script.version = EditorGUILayout.ObjectField("Version", script.version, typeof(VersionControlObject), true) as VersionControlObject;
        }
    }
    #endregion
}