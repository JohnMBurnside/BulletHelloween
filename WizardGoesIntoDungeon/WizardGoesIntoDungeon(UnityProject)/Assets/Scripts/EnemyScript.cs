using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    #region VARIABLES
    [Header("Movement Variables")]
    public Vector2 moveDir;
    public float moveSpeed = 3.0f;
    public float paceDuration = 3.0f;
    public float timer = 0;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        //SET MOVEMENT VARIABLES
        moveDir.Normalize();
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //MOVEMENT
        timer += Time.deltaTime;
        if (timer >= paceDuration)
        {
            moveDir *= -1;
            timer = 0;
        }
        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;
        //ANIMATION
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.Normalize();
        GetComponent<Animator>().SetFloat("x", velocity.x);
        GetComponent<Animator>().SetFloat("y", velocity.y);
    }
    #endregion
}
