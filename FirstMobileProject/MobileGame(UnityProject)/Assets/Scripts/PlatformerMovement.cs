#region NAMESPACES
using UnityEngine;
#endregion
public class PlatformerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;
    float moveX = 0;
    bool grounded = false;
    Animator anim;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    //void Start(){anim = GetComponent<Animator>();}
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveX * moveSpeed;
        GetComponent<Rigidbody2D>().velocity = velocity;
        float x = Input.GetAxisRaw("Horizontal");
        if (GetComponent<Rigidbody2D>().velocity.y == 0)
            grounded = true;
        if (GetComponent<Rigidbody2D>().velocity.y < 0 || GetComponent<Rigidbody2D>().velocity.y > 0)
            grounded = false;
        /*
         if(x == 0)
            anim.SetInteger("x", 0);
        else
            anim.SetInteger("x", 1);
        if(velocity.y > 0)
            anim.SetInteger("y", 1);
        else if (velocity.y < 0)
            anim.SetInteger("y", -1);
        else
            anim.SetInteger("y", 0);
        if(x > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        */
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            grounded = true;
            //anim.SetBool("grounded", grounded);
        }
    }
    #endregion
    #region ON TRIGGER STAY 2D FUNCTION
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            grounded = true;
            //anim.SetBool("grounded", grounded);
        }
    }
    #endregion
    #region ON TRIGGER EXIT 2D FUNCTION
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            grounded = false;
            //anim.SetBool("grounded", grounded);
        }
    }
    #endregion
    //PLATFORMER MOVEMENT FUNCTIONS
    #region JUMP FUNCTION
    public void Jump()
    {
        if(grounded == true)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * jumpSpeed));
    }
    #endregion
    #region MOVE LEFT FUNCTION
    public void MoveLeft(){moveX = -1;}
    #endregion
    #region MOVE RIGHT FUNCTION
    public void MoveRight(){moveX = 1;}
    #endregion
    #region STOP FUNCTION
    public void Stop(){moveX = 0;}
    #endregion
}
