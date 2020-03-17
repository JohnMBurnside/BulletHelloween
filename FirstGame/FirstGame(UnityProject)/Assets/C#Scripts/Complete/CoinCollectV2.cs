using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollectV2 : MonoBehaviour
{
    public int coins = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plus")
        {
            coins++;
            Destroy(collision.gameObject);
            if (coins > 9)
            {
                SceneManager.LoadScene("Win");
            }           
        }
        if (collision.gameObject.tag == "Minus")
        {
            coins--;
            Destroy(collision.gameObject);
            if(coins < -9)
            {
                SceneManager.LoadScene("Lose");
                Debug.Log("Hello");
            }
        }
    }
}