using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    //VARIABLES                         //VARIABLES
    [Header("General Settings")]        //GENERAL VARIABLES
    public Text creditsTitle;           //Credits title
    public Text credits;                //Credits
    public Text TFP;                    //Thanks For Playing text
    [Header("Credit Settings")]         //VARIABLES FOR CREDITS
    public Vector2 creditsDirection;    //What direction the credits will roll
    public float creditsSpeed = 1f;     //How fast the credits will roll
    public float creditsDuration = 10;  //How long the credits will roll
    public float timer;                 //Timer
    //UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        creditsTitle.GetComponent<Rigidbody2D>().velocity = creditsDirection * creditsSpeed;
        credits.GetComponent<Rigidbody2D>().velocity = creditsDirection * creditsSpeed;
        TFP.GetComponent<Rigidbody2D>().velocity = creditsDirection * creditsSpeed;
        if (timer > creditsDuration || Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Main Menu");
    }
}
///END OF SCRIPT!