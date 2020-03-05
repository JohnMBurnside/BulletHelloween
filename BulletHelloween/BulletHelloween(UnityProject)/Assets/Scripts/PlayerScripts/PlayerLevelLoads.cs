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
        else if (collision.gameObject.tag == "Boss")
            SceneManager.LoadScene("Boss Rush Final Boss");
        else if (collision.gameObject.tag == "Final Boss Room")
            SceneManager.LoadScene("Boss Rush Final Boss");
    }
}