using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IfStatements : MonoBehaviour
{
    //Global Variables
    public float coffeeTemp = 170.0f;
    float hotTemp = 180.0f;
    float coldTemp = 110.0f;
    void Update()
    {
        coffeeTemp -= Time.deltaTime * 5;
        //coffeeTemp = coffeTemp - Time.deltaTime * 5
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TemperatureTest();
        }
    }
    void TemperatureTest()
    {
        if (coffeeTemp > hotTemp)
        {
            Debug.Log("Ouch I burned my tounge");
        }
        else if (coffeeTemp < coldTemp)
        {
            Debug.Log("Cold coffee is gross.");
        }
        else
        {
            Debug.Log("Mmmmmmmmm coffee coffee coffee");
        }
    }
}