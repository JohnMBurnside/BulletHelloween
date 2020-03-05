using UnityEngine;
public class GridScript : MonoBehaviour
{
    #region VARIABLES
    [Header("Parallax Settings")]
    public float parallaxScale;
    Vector3 previousCamPos;
    Transform cameraTransform;
    GameObject mainCamera;
    #endregion
    //UNITY SCRIPTS
    #region START FUNCTION
    void Start()
    {
        //PERSPECTIVE CAMERA
        mainCamera = GameObject.Find("/Player/MainCamera");
        cameraTransform = Camera.main.transform;
        previousCamPos = cameraTransform.position;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //PERSPECTIVE CAMERA
        float parallax = (previousCamPos.x - cameraTransform.position.x) * parallaxScale;
        Vector3 pos = transform.position;
        pos.x += parallax;
        transform.position = pos;
        previousCamPos = cameraTransform.position;
    }
    #endregion
}