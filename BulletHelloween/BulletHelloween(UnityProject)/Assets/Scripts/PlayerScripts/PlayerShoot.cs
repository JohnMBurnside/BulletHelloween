#region NAMESPACES
using UnityEngine;
#endregion
public class PlayerShoot : MonoBehaviour
{
    #region VARIABLES
    [Header("General Variables")]               
    public GameObject prefab;
    public Joystick joystick;
    [Header("Shoot Variables")]                 
    public float shootDelay = 1.0f;            
    public float bulletSpeed = 10.0f;     
    public float bulletLifetime = 1.0f;         
    public float timer = 0f;                 
    [Header("Shoot Variable Upgrades")]           
    public float shootDelayUpgrade = 0.2f;      
    public float bulletSpeedUpgrade = 12f;     
    public float bulletLifetimeUpgrade = 2.0f;
    public Vector2 shootDir;
    #endregion
    //UNITY FUNCTIONS
    #region UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        Shoot();
    }
    #endregion
    //PLAYER SHOOT FUNCTIONS
    #region SHOOT FUNCTION
    public void Shoot()
    {
#if UNITY_STANDALONE
        if (Input.GetButton("Fire1") && timer > shootDelay)
        {
            timer = 0;
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 shootDir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            shootDir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
#endif
#if UNITY_ANDROID
        if ( joystick.Direction != new Vector2(0,0) && timer > shootDelay)
        {
            timer = 0;
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            shootDir = new Vector2(joystick.Direction.x, joystick.Direction.y);
            shootDir.Normalize();
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletLifetime);
        }
#endif
}
#endregion
}
