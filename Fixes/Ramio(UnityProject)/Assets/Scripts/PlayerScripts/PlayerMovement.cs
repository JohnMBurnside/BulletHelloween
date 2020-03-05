#region NAMESPACES
using System.Collections;
using UnityEngine;
#endregion
public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("Movement Settings")]
    public bool movementOn = true;
    public float moveSpeed = 10;
    public float jumpSpeed = 1;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.5f;
    [HideInInspector] public bool facingRight = true;
    public bool grounded;
    [Header("Animation Settings")]
    public Animator animator;
    public float jump_A_Duration;
    [Header("Slow Mo Settings")]
    public bool slowMoOn;
    [Header("Level 3")]
    public bool doggo = false;
    public Animator doggoAnim;
    [Header("Testing Only")]
    public bool cameraFollowTest;
    Transform camera;
    bool onStart;
    #endregion
    //UNTIY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        //Set camera variables
        if (cameraFollowTest == true)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            camera.transform.parent = null;
        }
        onStart = true;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //On Start
        if (onStart == true)
            StartCoroutine(OnStart());
        //Jumping
        if (Input.GetButtonDown("Jump") && grounded == true)
            StartCoroutine(Jump());
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (GetComponent<Rigidbody2D>().velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //Movement
        animator.SetBool("grounded", grounded);
        if(doggo == true)
            doggoAnim.SetBool("grounded", grounded);
        float moveX = Input.GetAxis("Horizontal");
        if (movementOn == true)
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.x = moveSpeed * moveX;
            if(GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
                GetComponent<Rigidbody2D>().velocity = velocity;
            animator.SetFloat("x", velocity.x);
            animator.SetFloat("y", velocity.y);
            if(doggo == true)
                doggoAnim.SetFloat("x", velocity.x);
        }
        //Flipping character
        if (moveX > 0 && !facingRight && movementOn)
            Flip();
        else if (moveX < 0 && facingRight && movementOn)
            Flip();
        //Camera movement
        if (cameraFollowTest == true)
            camera.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 0)
            grounded = true;
    }
    #endregion
    #region ON TRIGGER STAY 2D FUNCTION
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0)
            grounded = true;
    }
    #endregion
    #region ON TRIGGER EXIT 2D FUNCTION
    void OnTriggerExit2D(Collider2D collision) { grounded &= collision.gameObject.layer != 0; }
    #endregion
    //MOVEMENT FUNCTIONS
    #region JUMP FUNCTION
    IEnumerator Jump()
    {
        animator.SetTrigger("Jump");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(jump_A_Duration);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * jumpSpeed));
    }
    #endregion
    #region FLIP FUNCTION
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    #endregion
    #region ON START FUNCTION
    IEnumerator OnStart()
    {
        movementOn = false;
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = GetComponent<PlayerMovement>().moveSpeed * 1;
        GetComponent<Rigidbody2D>().velocity = velocity;
        animator.SetFloat("x", velocity.x);
        animator.SetFloat("y", velocity.y);
        yield return new WaitForSeconds(2f);
        movementOn = true;
        onStart = false;
        if (doggo == true)
        {
            GameObject dog = GameObject.Find("Player/Doggo");
            dog.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;       
        }
    }
    #endregion
}
