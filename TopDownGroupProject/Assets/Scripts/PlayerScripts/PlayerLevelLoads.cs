using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLevelLoads : MonoBehaviour
{
    //TRIGGER FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Win")
            SceneManager.LoadScene("Credits");
        else if (collision.gameObject.tag == "Level 1 Win")
            SceneManager.LoadScene("Level 2");
        else if (collision.gameObject.tag == "Level 2 Win")
            SceneManager.LoadScene("Level 3");
        else if (collision.gameObject.tag == "Level 3 Win")
            SceneManager.LoadScene("Level 4");
    }
}
