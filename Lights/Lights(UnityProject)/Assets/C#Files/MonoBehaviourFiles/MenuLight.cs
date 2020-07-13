#region NAMESPACES
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
#endregion
public class MenuLight : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    public float delay;
    [HideInInspector] public float timer;
    bool open = true;
    int intLoop;
    [Header("Reset Variables")]
    float orignalDelay;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start() { orignalDelay = delay; }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay && open == true)
        {
            for(int i = intLoop; i < 1;)
            {
                StartCoroutine(LightsF.LightIntensityLerp(0, 1, 1, gameObject));
                intLoop++;
                i = intLoop;
            }
            if(GetComponent<Light2D>().intensity == 1)
            {
                timer = 0;
                open = false;
            }
        }
        else if (timer > delay && open == false)
        {
            timer = 0;
            open = true;
            StartCoroutine(LightsF.LightIntensityLerp(1, 0, 1, gameObject));
        }
    }
    #endregion
    //MENU LIGHTS FUNCTION
    #region RESET LIGHTS FUNCTION
    public void ResetLights()
    {
        delay = orignalDelay;
        timer = 0;
        open = true;
        intLoop = 0;
        GetComponent<Light2D>().intensity = 0.001f;
    }
    #endregion
}
