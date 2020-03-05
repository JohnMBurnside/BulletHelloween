#region NAMESPACES
using UnityEngine;
#endregion
public class Parallax : MonoBehaviour
{
    #region VARIABLES
    [Header("Parallax Settings")]
    public Transform[] backgrounds;
    public float smoothing = 1f;
    private float[] parallaxScales; 
    private Transform cam;
    private Vector3 previousCamPos;
    #endregion
    //UNITY FUNCTIONS
    #region AWAKE FUNCTION
    void Awake(){cam = Camera.main.transform;}
    #endregion
    #region START FUNCTION
    void Start()
    {
        previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
            parallaxScales[i] = backgrounds[i].position.z * -1;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
    #endregion
}