#region NAMESPACES
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion
public class PlayerMonoBehaviour : MonoBehaviour
{
    #region VARIABLES
    [Header("Movement Settings")]
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;
    public float dashSpeed = 10.0f;
    Joystick joystick;
    bool grounded = false;
    float moveX;
    float moveY;
    public int jumpCount;
    [Header("Testing Settings")]
    public bool testingOnPc;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start() { joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>(); }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        moveX = joystick.Horizontal;
        moveY = joystick.Vertical;
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveX * moveSpeed;
        GetComponent<Rigidbody2D>().velocity = velocity;
        if (GetComponent<Rigidbody2D>().velocity.y == 0)
            grounded = true;
        if (GetComponent<Rigidbody2D>().velocity.y < 0 || GetComponent<Rigidbody2D>().velocity.y > 0)
            grounded = false;
        if (testingOnPc == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            if (Input.GetKeyDown(KeyCode.Q))
                Dash();
        }
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Death":
                GameObject.Find("MainCamera").GetComponent<CameraMonoBehavoir>().enabled = false;
                StartCoroutine(Death());
                break;
            case "Spikes":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "NextLevel":
                if (SceneManager.GetActiveScene().name == "Level 2")
                    SceneManager.LoadScene("Win");
                else
                    SceneManager.LoadScene("Level 2");
                break;
        }
    }
    #endregion
    #region ON COLLISION ENTER 2D FUNCTION
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            grounded = true;
            jumpCount = 0;
        }
    }
    #endregion
    #region ON COLLISION STAY 2D FUNCTION
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
            grounded = true;
    }
    #endregion
    #region ON COLLISION EXIT 2D FUNCTION
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
            grounded = false;
    }
    #endregion
    //PLAYER FUNCTIONS
    #region JUMP FUNCTION
    public void Jump()
    {
        if (grounded == true)
        {
            jumpCount++;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * jumpSpeed));
        }
    }
    #endregion
    #region DASH FUNCTION
    public void Dash() { StartCoroutine(DashC()); }
    #endregion
    #region DASH C FUNCTION
    IEnumerator DashC()
    {
        if(jumpCount < 2)
        {
            jumpCount = jumpCount + 2;
            float prevMoveSpeed = moveSpeed;
            moveSpeed = dashSpeed * 10;
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.x = moveX * moveSpeed / 3;
            velocity.y = moveY * moveSpeed / 7;
            GetComponent<Rigidbody2D>().velocity = velocity;
            yield return new WaitForSeconds(.05f);
            moveSpeed = prevMoveSpeed;
        }
    }
    #endregion
    #region DEATH FUNCTION
    public IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
