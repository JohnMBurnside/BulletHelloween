using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelTitles : MonoBehaviour
{
    //VARIABLES
    public float timer = 0;
    public float onTime = 5;
    //START FUNCTION
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }
    //UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0 && timer < onTime)
            GetComponent<Canvas>().enabled = true;
        else
            GetComponent<Canvas>().enabled = false;
    }
}