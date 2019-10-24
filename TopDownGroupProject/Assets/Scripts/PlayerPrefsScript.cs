using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsScript : MonoBehaviour
{
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        number = PlayerPrefs.GetInt("number");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            number--;
            PlayerPrefs.SetInt("number", number);
        }
    }
}
