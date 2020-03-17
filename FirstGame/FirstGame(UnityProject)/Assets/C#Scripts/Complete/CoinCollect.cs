using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollect : MonoBehaviour
{
    public int coinCount = 0;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plus")
        {
            coinCount++;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Minus")
        {
            coinCount--;
            Destroy(collision.gameObject);
        }

    }
    void WinLose()
    {
        if (coinCount > 9)
        {
            SceneManager.LoadScene("Lose");
        }
        else if(coinCount < -9)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
