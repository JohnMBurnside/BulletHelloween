#region NAMESPACES
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
#endregion
public class DarkEnemyAI : MonoBehaviour
{
    #region VARIABLES
    [Header("Movement Settings")]
    public Transform player;
    public float speed = .1f;
    public float moveX;
    public bool facingLeft = true;
    public Vector2 moveDir;
    [Header("Lighting Settings")]
    public float lightOuterRadius;
    bool lightStart = true;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        transform.parent = null;
        if (player.gameObject.GetComponent<PlayerMovement>().doggo == true)
            speed = 3.75f;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if(GameObject.Find("Player").GetComponent<PlayerCollision>().turnAway == true)
            moveDir = new Vector2(-1, 0);
        else if (player.transform.position.x - gameObject.transform.position.x > 0)
            moveDir = new Vector2(1, 0);
        else
            moveDir = new Vector2(-1, 0);
        GetComponent<Rigidbody2D>().velocity = moveDir * speed;
        moveX = GetComponent<Rigidbody2D>().velocity.x;
        if (moveX > 0 && !facingLeft)
            Flip();
        else if (moveX < 0 && facingLeft)
            Flip();
        if(lightStart == true)
        {
            GetComponentInChildren<Light2D>().pointLightOuterRadius = lightOuterRadius;
            lightOuterRadius += .01f;
            if (GetComponentInChildren<Light2D>().pointLightOuterRadius >= 1)
                lightStart = false;
        }
        else
        {
            GetComponentInChildren<Light2D>().pointLightOuterRadius = lightOuterRadius;
            lightOuterRadius -= .01f;
            if(GetComponentInChildren<Light2D>().pointLightOuterRadius <= 0)
                lightOuterRadius += .01f;
        }
    }
    #endregion
    //DARK ENEMY AI FUNCTIONS
    #region FLIP FUNCTION
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    #endregion
}