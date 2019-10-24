using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Coins : MonoBehaviour
{
    public int coinCount = 0;
    public Text coinText;
     void Start()
    {
        coinText.text = "Coins:" +  coinCount;
        //string foo = "" + coinCount +" = Coins";
    }


     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coinCount++;
            coinText.text = "Coins: " + coinCount;
            Destroy(collision.gameObject);
            if (coinCount == 5)
                SceneManager.LoadScene("Win");
            {

            }
        }
    }
}
        
 