using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    public string area;
    public int chapter;
    public float timer;
    #pragma warning disable CS0108
    public Rigidbody2D rigidbody;
    #pragma warning restore CS0108
    public GameObject[] triggers;
    [Header("Health Settings")]
    public float maxHealth;
    public float currentHealth;
    public float timeToHeal;
    public bool heal = true;
    [Header("Shooting Settings")]
    public GameObject[] guns = new GameObject[2];
    public int bulletUnlocks;
    public Gun currentGun;
    public Gun[] gunInventory;
    [Header("Movement Setting")]
    public float moveX;
    public float moveSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public float groundCheckRaduis;
    public LayerMask ground;
    public bool inputOn = true;
    [Header("Camera Settings")]
    public Camera playerCamera;
    public GameObject cameraOffsetObject;
    [Header("Arm Settings")]
    public GameObject[] armTargets = new GameObject[2];
    public Vector2 defaultArmPos;
    [Header("Animation Settings")]
    public Animator playerAnimator;
    [Header("*UI Connection*")]
    public GameObject playerHUD;
    public UI fadeUI;
    [Header("Private Variables")]
    Vector2 velocity;
    float healTimer;
    bool death;
    bool facingRight = true;
    bool grounded = true;
    #endregion
    #region START FUNCTION
    void Start()
    {
        LoadGame();
        SaveSystem.Save(this);
        Cursor.visible = false;
        PlayerPrefs.GetInt("triggerIndex");
        if (SceneManager.GetActiveScene().name != "Tutorial")
            transform.position = triggers[PlayerPrefs.GetInt("triggerIndex")].transform.position;
        area = SceneManager.GetActiveScene().name;
        WeaponSwitch(0);
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if (currentHealth < 0)
            Death();
        else if (Time.timeScale != 0 && inputOn)
        {
            Movement(true);
            ArmMovement();
            Shoot();
            WeaponSelection(bulletUnlocks);
            Heal();
            Animate();
        }
        else
            Movement(false);
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletUnlock"))
        {
            bulletUnlocks++;
            playerHUD.GetComponent<UI>().UnlockBullets(bulletUnlocks);
            Destroy(collision.gameObject);
            SaveSystem.Save(this);
        }
    }
    #endregion
    #region ON DRAW GIZMOS FUNCTION
    void OnDrawGizmos()
        { Gizmos.DrawWireSphere(groundCheck.position, groundCheckRaduis); }
    #endregion
    #region LOAD GAME FUNCTION
    public void LoadGame()
    {
        PlayerData data = SaveSystem.Load();
        if (data != null)
        {
            area = data.area;
            bulletUnlocks = data.bulletUnlocks;
            chapter = data.chapter;
            playerHUD.GetComponent<UI>().UnlockBullets(bulletUnlocks);
        }
    }
    #endregion
    #region MOVEMENT FUNCTION
    public void Movement(bool playerControl)
    {
        if(playerControl)
        {
            playerCamera.gameObject.transform.position = new Vector3(cameraOffsetObject.transform.position.x, cameraOffsetObject.transform.position.y, -10);
            moveX = Input.GetAxis("Horizontal");
        }
        velocity = rigidbody.velocity;
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRaduis, ground);
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            velocity = Vector2.up * jumpForce;
        if (Input.GetKey(KeyCode.LeftShift))
            velocity.x = moveSpeed * moveX * 2;
        else
            velocity.x = moveSpeed * moveX;
        rigidbody.velocity = velocity;
    }
    #endregion
    #region ARM MOVEMENT FUNCTION
    void ArmMovement()
    {
        if (inputOn)
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (Input.GetButtonDown("Fire2"))
                armTargets[0].transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            else if (Input.GetButton("Fire2"))
                armTargets[1].transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            else
            {
                foreach (GameObject arm in armTargets)
                    arm.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                if (mousePosition.x < transform.localPosition.x && facingRight)
                {
                    facingRight = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (mousePosition.x > transform.localPosition.x && facingRight == false)
                {
                    facingRight = true;
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
        else
        {
            foreach (GameObject arm in armTargets)
                arm.transform.localPosition = new Vector3(defaultArmPos.x, defaultArmPos.y, 0);
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    #endregion
    #region SHOOT FUNCTION
    void Shoot()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer > currentGun.fireRate)
        {
            timer = 0;
            GameObject bulletRight = Instantiate(currentGun.bulletPrefab, guns[0].transform.position, guns[0].transform.rotation);
            GameObject bulletLeft = Instantiate(currentGun.bulletPrefab, guns[1].transform.position, guns[1].transform.rotation);
            bulletRight.GetComponent<Bullet>().damage = currentGun.damage;
            bulletLeft.GetComponent<Bullet>().damage = currentGun.damage;
            if (facingRight)
            {
                bulletRight.GetComponent<Rigidbody2D>().AddForce(guns[0].transform.right * currentGun.bulletSpeed, ForceMode2D.Impulse);
                bulletLeft.GetComponent<Rigidbody2D>().AddForce(guns[1].transform.right * currentGun.bulletSpeed, ForceMode2D.Impulse);
            }
            else
            {
                bulletRight.GetComponent<Rigidbody2D>().AddForce(guns[0].transform.right * -currentGun.bulletSpeed, ForceMode2D.Impulse);
                bulletLeft.GetComponent<Rigidbody2D>().AddForce(guns[1].transform.right * -currentGun.bulletSpeed, ForceMode2D.Impulse);
            }
            Destroy(bulletRight, currentGun.bulletLifetime);
            Destroy(bulletLeft, currentGun.bulletLifetime);
        }
    }
    #endregion
    #region WEAPON SELECTION FUNCTION
    void WeaponSelection(int unlocks)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && unlocks > 0)
            WeaponSwitch(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) && unlocks > 1)
            WeaponSwitch(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) && unlocks > 2)
            WeaponSwitch(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4) && unlocks > 3)
            WeaponSwitch(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5) && unlocks > 4)
            WeaponSwitch(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6) && unlocks > 5)
            WeaponSwitch(5);
    }
    #endregion
    #region WEAPON SWITCH FUNCTION
    void WeaponSwitch(int gunToSwitch)
    {
        foreach (GameObject gun in guns)
            gun.transform.eulerAngles = new Vector3(0, 0, gun.transform.eulerAngles.z - currentGun.rotation);
        currentGun = gunInventory[gunToSwitch];
        foreach (GameObject gun in guns)
        {
            gun.GetComponent<SpriteRenderer>().sprite = currentGun.gunSprite;
            gun.transform.localPosition = new Vector3(0 + currentGun.position.x, 0 + currentGun.position.y, 0 + currentGun.position.z);
            gun.transform.eulerAngles = new Vector3(0, 0, gun.transform.eulerAngles.z + currentGun.rotation);
            float scale = currentGun.scale;
            if (scale < 1)
                scale = (1 - currentGun.scale) * -1;
            gun.transform.localScale = new Vector3(1 + scale, 1 + scale);
        }
    }
    #endregion
    #region HEAL FUNCTION
    void Heal()
    {
        if (heal)
        {
            healTimer += Time.deltaTime;
            if (healTimer > timeToHeal && currentHealth < maxHealth)
            {
                if (currentHealth < 50)
                    currentHealth += .1f;
                else
                    currentHealth += .3f;
            }
        }
        else
        {
            healTimer = 0;
            heal = true;
        }
    }
    #endregion
    #region DEATH FUNCTION
    void Death()
    {
        if (death == false)
        {
            rigidbody.velocity = new Vector2(0, velocity.y);
            rigidbody.constraints = RigidbodyConstraints2D.None;
            death = true;
            playerAnimator.SetBool("death", true);
            StartCoroutine(fadeUI.FadeOut("GameOver", true, PlayerPrefs.GetInt("triggerIndex")));
        }
    }
    #endregion
    #region ANIMATE FUNCTION
    void Animate()
    {
        if (currentHealth < 0)
            playerAnimator.SetTrigger("death");
        else
        {
            playerAnimator.SetFloat("x", velocity.x);
            playerAnimator.SetFloat("y", velocity.y);
            playerAnimator.SetBool("grounded", grounded);
            playerAnimator.SetBool("facingRight", facingRight);
        }
    }
    #endregion
}
