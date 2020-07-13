using System.Threading;
using UnityEditor;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    #region VARIABLES 
    [Header("General Settings")]
    public GameObject target;
    public Vector3 scaling;
    [Header("Health Settings")]
    public float currentHealth;
    public LayerMask deathMask;
    [Header("Tracking Settings")]
    public GameObject armTarget;
    public float viewRadius;
    [Range(0, 360)] public float viewAngle;
    public float shotAtRange;
    public bool shotAt;
    public bool facingRight;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    [Header("Bullet Settings")]
    public Transform gun;
    public GameObject bulletPrefab;
    public float bulletDamage;
    public float bulletSpeed;
    public float fireRate;
    public float bulletLifetime;
    public float timer;
    [Header("Animation Settings")]
    public Animator animator;


    public float bossTimer;
    public bool boss;
    public float speed;
    #endregion
    #region START FUNCTION
    void Start()
    {
        if(facingRight)
            transform.localScale = scaling;
        else
            transform.localScale = new Vector3(-scaling.x, scaling.y, scaling.z);
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if (currentHealth < 0)
            Death();
        else
            FindPlayer();
        if (boss)
        {
            bossTimer += Time.deltaTime;
            if (bossTimer > 0)
            {
                if (gun.transform.eulerAngles.z < 90 || gun.transform.eulerAngles.z < 0 || gun.transform.eulerAngles.z > -90)
                    gun.transform.eulerAngles = new Vector3(0, 0, gun.transform.eulerAngles.z + .1f * speed);
                else if (gun.transform.eulerAngles.z > 90 || gun.transform.eulerAngles.z < -91)
                    gun.transform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
    }
    #endregion
    #region ON DRAW GIZMOS SELECTED FUNCTION
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shotAtRange);
    }
    #endregion
    #region DEATH FUNCTION
    void Death()
    {
        if(!boss)
        {
            float random = Random.Range(0, 1);
            animator.SetTrigger("death" + random.ToString());
            gameObject.layer = 13;        
            gameObject.tag = "Untagged";
            Destroy(this);
        }
    }
    #endregion
    #region SHOOT FUNCTION
    void Shoot()
    {
        if(timer > fireRate)
        {
            timer = 0;
            Rigidbody2D bullet = Instantiate(bulletPrefab, gun.position, gun.rotation).GetComponent<Rigidbody2D>();
            bullet.gameObject.GetComponent<Bullet>().damage = bulletDamage;
            if (facingRight)
                bullet.AddForce(gun.right * bulletSpeed, ForceMode2D.Impulse);
            else
                bullet.AddForce(gun.right * -bulletSpeed, ForceMode2D.Impulse);
            Destroy(bullet, bulletLifetime);
        }
    }
    #endregion
    #region FIND PLAYER FUNCTION
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
    void FindPlayer()
    {
        Collider2D objectInView = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayer);
        if (objectInView != null || (Physics2D.OverlapCircle(transform.position, shotAtRange, playerLayer) && shotAt))
        {
            if (objectInView == null)
                objectInView = Physics2D.OverlapCircle(transform.position, shotAtRange, playerLayer);
            timer += Time.deltaTime;
            target = objectInView.gameObject;
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            armTarget.transform.position = target.transform.position;
            if (target.transform.position.x < transform.localPosition.x && facingRight)
            {
                facingRight = false;
                transform.localScale = new Vector3(-scaling.x, scaling.y, scaling.z);
            }
            else if (target.transform.position.x > transform.localPosition.x && facingRight == false)
            {
                facingRight = true;
                transform.localScale = scaling;
            }
            if (Vector3.Angle(transform.right, directionToTarget) < viewAngle / 2 && facingRight)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, groundLayer))
                    Shoot();
            }
            else if (Vector3.Angle(-transform.right, directionToTarget) < viewAngle / 2 && !facingRight)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, groundLayer))
                    Shoot();
            }
        }
        else
            objectInView = null;
    }
    #endregion
    #region DIRECTION FROM ANGLE FUNCTION
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            if (transform.localScale.x < 0)
                angleInDegrees += 180;                
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
    #endregion
}
