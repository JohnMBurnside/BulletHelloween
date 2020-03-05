#region NAMESPACES
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion
public class Ending : MonoBehaviour
{
    #region VARIABLES
    [Header("Ending Settings")]
    public float endingLength;
    public float moveSpeed = 3;
    bool end = true;
    public float timer;
    #endregion
    //UNITY FUNCTION
    #region UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        if(end == true)
        {
            StartCoroutine(ENDING());
            end = false;
        }
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveSpeed * 1;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
    #endregion
    //ENDING FUNCTION
    #region ENDING FUNCTION
    IEnumerator ENDING()
    {
        yield return new WaitForSeconds(endingLength);
        SceneManager.LoadScene("Main Menu");
    }
    #endregion
}
