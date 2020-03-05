using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    //VARIABLES							//VARIABLES
    [Header("General Settings")]        //GENERAL VARIABLES
    public GameObject creditsCanvas;    //Credit canvas
    public Text creditsTitle;           //Credits title
    public Text credits;                //Credits
    public Text note;                   //Note
    [Header("Credit Settings")]         //CREDIT VARIABLES
    public Vector2 creditsDirection;    //What direction the credits will roll
    public float creditsSpeed = 1f;     //How fast the credits will roll
    public bool creditsRoll = false;    //Tells whether the credits can roll or not
    public float creditsDuration = 10;  //How long the credits will roll
    Vector2 creditsTitleHome;           //Where the title starts from
    Vector2 creditsHome;                //Where the credits start from
    public float timer;                 //Timer
    //START FUNCTION
    void Start()
    {
        creditsTitleHome = creditsTitle.GetComponent<Transform>().position;
        creditsHome = credits.GetComponent<Transform>().position;
    }
    //UPDATE
    void Update()
    {
        CreditsRoll();
        if (creditsRoll == true)
            GetComponent<Canvas>().enabled = false;
        else
        {
            creditsTitle.GetComponent<Transform>().position = creditsTitleHome;
            credits.GetComponent<Transform>().position = creditsHome;
            timer = 0;
            GetComponent<Canvas>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            creditsRoll = false;
        if (timer > creditsDuration)
            creditsRoll = false;
    }
    //START GAME FUNCTION
    public void StartGame()
    {
        PlayerPrefs.SetInt("lives", 3);
        SceneManager.LoadScene("Level 1");
    }
    //BOSS RUSH FUNCTION
    public void BossRush()
    {
        PlayerPrefs.SetInt("lives", 3);
        SceneManager.LoadScene("Boss Rush");
    }
    //CREDITS FUNCTION
    public void Credits()
    {
        creditsRoll = true;
    }
    //QUIT FUNCTION
    public void Quit()
    {
        Application.Quit();
    }
    //CREDITS ROLL FUNCTION
    void CreditsRoll()
    {
        if (creditsRoll == true)
        {
            timer += Time.deltaTime;
            note.GetComponent<Text>().enabled = true;
            creditsTitle.GetComponent<Text>().enabled = true;
            credits.GetComponent<Text>().enabled = true;
            creditsCanvas.GetComponent<Canvas>().enabled = true;
            creditsTitle.GetComponent<Rigidbody2D>().velocity = creditsDirection * creditsSpeed;
            credits.GetComponent<Rigidbody2D>().velocity = creditsDirection * creditsSpeed;
        }
       else
        {
            creditsCanvas.GetComponent<Canvas>().enabled = false;
            note.GetComponent<Text>().enabled = false;
            creditsTitle.GetComponent<Text>().enabled = false;
            credits.GetComponent<Text>().enabled = false;
        }
    }
}
///END OF SCRIPT!