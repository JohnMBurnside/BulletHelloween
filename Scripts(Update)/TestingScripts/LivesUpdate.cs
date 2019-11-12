using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LivesUpdate : MonoBehaviour
{
    /// <summary>
    /// FOR TESTING PURPOSES
    /// </summary>
    //UPDATE FUNCTION
    void Update()
    {
        PlayerPrefs.SetInt("lives", 3);
    }
}
///END OF SCRIPT!