#region NAMESPACES
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion
public class CameraZoom : MonoBehaviour
{
    #region VARIABLES
    [Header("Camera Settings")]
    public float zoomed = 1.6f;
    public float normal = 4f;
    public float slowMoZoom = 2;
    public float smooth = 5;
    #endregion
    #region START FUNCTION
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Level 2")
            normal = 1.6f;
        if (SceneManager.GetActiveScene().name == "Level 2")
            slowMoZoom = 4.4f;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if (GetComponentInParent<PlayerCollision>().camZoom == true)
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoomed, Time.deltaTime * smooth);
        else if(GetComponentInParent<PlayerSlowMo>().slowMoOn == true)
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, slowMoZoom, Time.deltaTime * smooth);
        else
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, normal, Time.deltaTime * smooth);
    }
    #endregion
}
