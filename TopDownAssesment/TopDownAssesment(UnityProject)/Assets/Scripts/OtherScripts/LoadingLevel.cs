using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingLevel : MonoBehaviour
{
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LevelTwo")
        {
            SceneManager.LoadScene("LevelTwo");
        }
        else if (collision.gameObject.tag == "Win")
        {
            SceneManager.LoadScene("Win");
        }
    }

}
