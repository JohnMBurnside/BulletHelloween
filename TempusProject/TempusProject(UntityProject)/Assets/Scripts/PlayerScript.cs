using UnityEngine;
using Tempus;
public class PlayerScript : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    public int level;
    public int tempusLevel;
    public EnemyBattleData enemyData;
    [Header("Movement Settings")]
    public float jumpSpeed = 5.0f;
    public float moveSpeed = 5.0f;
    public float fallMultiplier = 5.0f;
    public float lowJumpMultiplier = 2.5f;
    public bool grounded;
    Rigidbody2D playerRigidbody;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        //MOVEMENT
        if(GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
            Movement();
    }
    #endregion
    #region ON COLLISION ENTER 2D FUNCTION
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            enemyData = new EnemyBattleData(true, collision.gameObject.GetComponent<EnemyScript>().enemyType, collision.gameObject.GetComponent<EnemyScript>().level, collision.gameObject);
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        //JUMPING
        if (collision.gameObject.layer == 0)
            grounded = true;
    }
    #endregion
    #region ON TRIGGER STAY 2D FUNCTION
    void OnTriggerStay2D(Collider2D collision)
    {
        //JUMPING
        if (collision.gameObject.layer == 0)
            grounded = true;
    }
    #endregion
    #region ON TRIGGER EXIT FUNCTION
    void OnTriggerExit2D(Collider2D collision)
    {
        //JUMPING
        grounded &= collision.gameObject.layer != 0;
    }
    #endregion
    //PLAYER FUNCTIONS
    #region MOVEMENT FUNCTION
    void Movement()
    {
        //MOVEMENT
        float moveX = Input.GetAxis("Horizontal");
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveSpeed * moveX;
        GetComponent<Rigidbody2D>().velocity = velocity;
        //JUMPING
        if (Input.GetButtonDown("Jump") && grounded == true)
            Jump();
        if (playerRigidbody.velocity.y < 0)
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (playerRigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }
    #endregion
    #region JUMP FUNCTION
    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
    }
    #endregion
}