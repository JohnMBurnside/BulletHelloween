#region NAMESPACES
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
#endregion
public class MenuLighting : MonoBehaviour
{
    #region VARIABLES
    [Header("Menu Lights Array")]
    public GameObject mainMenuCanvas;
    GameObject[] arrays;
    GameObject[] arrayOne;
    GameObject[] arrayTwo;
    GameObject[] arrayThree;
    [HideInInspector] public bool exitMenu;
    [Header("Animation Settings")]
    public Animator fade;
    float timer;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        //Define animator
        fade = GameObject.Find("UI/Fade/BlackOverlay").GetComponent<Animator>();
        fade.SetTrigger("FadeIn"); 
        //Define variable mainMenuCanvas
        mainMenuCanvas = GameObject.Find("UI/MainMenu");
        //Defining array size
        arrays = new GameObject[transform.childCount];
        //Assiging the array objects for use to get the children
        for (int i = 0; i < transform.childCount; i++)
            arrays[i] = transform.GetChild(i).gameObject;
        //Defining array size
        arrayOne = new GameObject[arrays[0].transform.childCount];
        arrayTwo = new GameObject[arrays[1].transform.childCount];
        arrayThree = new GameObject[arrays[2].transform.childCount];
        //Assigning the lights to the arrays
        for(int i = 0; i < arrays[0].transform.childCount; i++)
        {
            arrayOne[i] = arrays[0].transform.GetChild(i).gameObject;
            arrayOne[i].GetComponent<MenuLight>().enabled = true;
        }
        for (int i = 0; i < arrays[1].transform.childCount; i++)
        {
            arrayTwo[i] = arrays[1].transform.GetChild(i).gameObject;
            arrayTwo[i].GetComponent<MenuLight>().enabled = false;
        }
        for (int i = 0; i < arrays[2].transform.childCount; i++)
        {
            arrayThree[i] = arrays[2].transform.GetChild(i).gameObject;
            arrayThree[i].GetComponent<MenuLight>().enabled = false;
        }
        arrayOne[0].GetComponent<Light2D>().intensity = 0.001f;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            if (timer > 1 && timer < 1.1f)
                fade.gameObject.SetActive(false);
            if (arrayOne[0].GetComponent<Light2D>().intensity < .01f
                && arrayOne[arrayOne.Length - 1].GetComponent<Light2D>().intensity < .01f
                && arrayTwo[0].GetComponent<Light2D>().intensity < .01f
                && arrayTwo[arrayTwo.Length - 1].GetComponent<Light2D>().intensity < .01f
                && arrayThree[0].GetComponent<Light2D>().intensity < .01f
                && arrayThree[arrayThree.Length - 1].GetComponent<Light2D>().intensity < .01f
                && exitMenu == true)
            {
                fade.gameObject.SetActive(true);
                if (mainMenuCanvas.GetComponent<UI>().continueGame == true)
                    StartCoroutine(UniversalF.LoadLevel(SaveSystem.Load().level, fade));
                else if (mainMenuCanvas.GetComponent<UI>().newGame == true)
                    StartCoroutine(UniversalF.LoadLevel(0, fade));
                else if (mainMenuCanvas.GetComponent<UI>().levelButton == true)
                    StartCoroutine(UniversalF.LoadLevel(mainMenuCanvas.GetComponent<UI>().levelSelectNumber, fade));
                else if (mainMenuCanvas.GetComponent<UI>().quitGame == true)
                    Application.Quit();
            }
            if (exitMenu == false)
            {
                if (arrayOne[0].GetComponent<Light2D>().intensity == 0 && arrayOne[arrayOne.Length - 1].GetComponent<Light2D>().intensity <= .5f && arrayTwo[0].GetComponent<MenuLight>().enabled == false)
                {
                    foreach (GameObject light in arrayTwo)
                        light.GetComponent<MenuLight>().enabled = true;
                    foreach(GameObject light in arrayOne)
                    {
                        light.GetComponent<MenuLight>().timer = 0;
                        light.GetComponent<MenuLight>().enabled = false;
                        if (exitMenu == true)
                            break;
                    }
                    foreach (GameObject light in arrayThree)
                        light.GetComponent<MenuLight>().ResetLights();
                }
                else if (arrayTwo[0].GetComponent<Light2D>().intensity == 0 && arrayTwo[arrayTwo.Length - 1].GetComponent<Light2D>().intensity <= .5f && arrayThree[0].GetComponent<MenuLight>().enabled == false)
                {
                    foreach (GameObject light in arrayThree)
                        light.GetComponent<MenuLight>().enabled = true;
                    foreach (GameObject light in arrayTwo)
                    {
                        light.GetComponent<MenuLight>().timer = 0;
                        light.GetComponent<MenuLight>().enabled = false;
                        if (exitMenu == true)
                            break;
                    }
                    foreach(GameObject light in arrayOne)
                        light.GetComponent<MenuLight>().ResetLights();
                }
                else if (arrayThree[0].GetComponent<Light2D>().intensity == 0 && arrayThree[arrayTwo.Length - 1].GetComponent<Light2D>().intensity <= .5f && arrayOne[0].GetComponent<MenuLight>().enabled == false)
                {
                    foreach (GameObject light in arrayOne)
                        light.GetComponent<MenuLight>().enabled = true;
                    foreach (GameObject light in arrayThree)
                    {
                        light.GetComponent<MenuLight>().timer = 0;
                        light.GetComponent<MenuLight>().enabled = false;
                        if (exitMenu == true)
                            break;
                    }
                    foreach (GameObject light in arrayTwo)
                        light.GetComponent<MenuLight>().ResetLights();
                }
            }
        }
    }
    #endregion
    //MENU LIGHTING FUNCTIONS
    #region EXIT MAIN MENU FUNCTION
    public void ExitMainMenu()
    {
        exitMenu = true;
        foreach(GameObject light in arrayOne)
        {
            light.GetComponent<MenuLight>().enabled = false;
            light.GetComponent<Light2D>().StartCoroutine(LightsF.LightIntensityLerp(light.GetComponent<Light2D>().intensity, 0.001f, 2, light, true));
        }
        foreach (GameObject light in arrayTwo)
        {
            light.GetComponent<MenuLight>().enabled = false;
            light.GetComponent<Light2D>().StartCoroutine(LightsF.LightIntensityLerp(light.GetComponent<Light2D>().intensity, 0.001f, 2, light, true));
        }
        foreach (GameObject light in arrayThree)
        {
            light.GetComponent<MenuLight>().enabled = false;
            light.GetComponent<Light2D>().StartCoroutine(LightsF.LightIntensityLerp(light.GetComponent<Light2D>().intensity, 0.001f, 2, light, true));
        }
    }
    #endregion
}