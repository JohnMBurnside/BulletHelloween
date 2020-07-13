#region NAMESPACES
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
#endregion
public class SafeLight : MonoBehaviour
{
    #region VARIABLES
    [Header("Light Settings")]
    public LightsType lightType;
    #endregion
    //UNITY FUNCTION
    #region START FUNCTION
    void Start()
    {
        //Collision
        try 
            { GetComponent<CircleCollider2D>().radius = GetComponent<Light2D>().pointLightOuterRadius; }
        catch 
            { gameObject.AddComponent<CircleCollider2D>().radius = GetComponent<Light2D>().pointLightOuterRadius; }
    }
    #endregion
}
