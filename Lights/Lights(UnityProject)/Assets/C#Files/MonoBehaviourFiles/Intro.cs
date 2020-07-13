#region NAMESPACES
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#endregion
public class Intro : MonoBehaviour
{
    #region VARIABLES
    public Text[] introText = new Text[5];
    float timer;
    public bool[] textBool = new bool[4];
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        foreach (Text text in introText)
        {
            text.gameObject.SetActive(false);
            text.gameObject.GetComponent<Animator>().enabled = false;
        }
        for (int i = 0; i < textBool.Length; i++)
            textBool[i] = true;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0 && timer < 2)
        {
            introText[0].gameObject.SetActive(true);
            introText[0].gameObject.GetComponent<Animator>().enabled = true;
        }
        else if (timer > 2 && timer < 4)
        {
            if (textBool[0] == true)
            {
                foreach (Text text in introText)
                {
                    text.gameObject.SetActive(false);
                    text.gameObject.GetComponent<Animator>().enabled = false;
                }
                textBool[0] = false;
            }
            introText[1].gameObject.SetActive(true);
            introText[1].gameObject.GetComponent<Animator>().enabled = true;
        }
        else if (timer > 4 && timer < 6)
        {
            if (textBool[1] == true)
            {
                foreach (Text text in introText)
                {
                    text.gameObject.SetActive(false);
                    text.gameObject.GetComponent<Animator>().enabled = false;
                }
                textBool[1] = false;
            }
            introText[2].gameObject.SetActive(true);
            introText[2].gameObject.GetComponent<Animator>().enabled = true;
        }
        else if (timer > 6 && timer < 8.50f)
        {
            if (textBool[2] == true)
            {
                foreach (Text text in introText)
                {
                    text.gameObject.SetActive(false);
                    text.gameObject.GetComponent<Animator>().enabled = false;
                }
                textBool[2] = false;
            }
            introText[3].gameObject.SetActive(true);
            introText[3].gameObject.GetComponent<Animator>().enabled = true;
        }
        else if (timer > 8.50f && timer < 13.50f)
        {
            if (textBool[3] == true)
            {
                foreach (Text text in introText)
                {
                    text.gameObject.SetActive(false);
                    text.gameObject.GetComponent<Animator>().enabled = false;
                }
                textBool[3] = false;
            }
            introText[4].gameObject.SetActive(true);
            introText[4].gameObject.GetComponent<Animator>().enabled = true;
        }
        else if (timer > 14.50f)
            SceneManager.LoadScene("Level 1");
    }
    #endregion
}
