using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum BulletType
{
    Default = 1,
    Bomb = 2,
    Impulse = 3
}
public class Bullet : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    public BulletType type;
    public float damage;
    [Header("Particle Effect Settings")]
    public GameObject particleEffect;
    public float particleLifetime;
    [Header("Bomb Settings")]
    public float detonateTime;
    public float bombRange;
    bool onGround;
    [Header("Impulse Settings")]
    public float blastPower;
    #endregion
    #region START FUNCTION
    void Start()
    {
        if(type == BulletType.Bomb || type == BulletType.Impulse)
            StartCoroutine(BombExplode(true));
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject particle;
        if (type == BulletType.Default)
        {
            switch (collision.gameObject.tag)
            {
                case "Player":
                    UniversalTakeDamage(collision.gameObject, damage);
                    particle = Instantiate(particleEffect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5), Quaternion.identity);
                    Destroy(particle, particleLifetime);
                    Destroy(gameObject);
                    break;
                case "Enemy":
                    UniversalTakeDamage(collision.gameObject, damage);
                    particle = Instantiate(particleEffect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5), Quaternion.identity);
                    Destroy(particle, particleLifetime);
                    Destroy(gameObject);
                    break;
                case "Destructible":
                    Destroy(collision.gameObject);
                    particle = Instantiate(particleEffect, new Vector3(collision.transform.position.x, collision.transform.position.y, -5), Quaternion.identity);
                    Destroy(particle, particleLifetime);
                    Destroy(gameObject);
                    break;
                case "Button":
                    collision.gameObject.GetComponentInParent<Animator>().SetTrigger("buttonPressed");
                    if (SceneManager.GetActiveScene().name == "Tutorial")
                        GameObject.Find("Tutorial").GetComponent<Tutorial>().ButtonHit(collision.gameObject.transform.parent.name);
                    particle = Instantiate(particleEffect, new Vector3(collision.transform.position.x, collision.transform.position.y, -5), Quaternion.identity);
                    Destroy(particle, particleLifetime);
                    Destroy(gameObject);
                    break;
                default:
                    particle = Instantiate(particleEffect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5), Quaternion.identity);
                    Destroy(particle, particleLifetime);
                    Destroy(gameObject);
                    break;
            }
        }
    }
    #endregion
    #region ON COLLISION ENTER 2D FUNCTION
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject particle;
        if (type == BulletType.Bomb)
        {
            switch (collision.gameObject.tag)
            {
                case "Destructible":
                    if (!onGround)
                    {
                        Destroy(collision.gameObject);
                        particle = Instantiate(particleEffect, new Vector3(collision.transform.position.x, collision.transform.position.y, -5), Quaternion.identity);
                        StartCoroutine(BombExplode(false));
                        Destroy(particle, particleLifetime);
                    }
                    break;
                default:
                    onGround = true;
                    break;
            }
        }
    }
    #endregion
    #region ON DRAW GIZMOS FUNCTION
    void OnDrawGizmos()
    { 
        if(type != BulletType.Default)
            Gizmos.DrawWireSphere(transform.position, bombRange); 
    }
    #endregion
    #region BOMB EXPLODE FUNCTION
    IEnumerator BombExplode(bool wait)
    {
        if(wait)
        {
            yield return new WaitForSeconds(detonateTime);
            GameObject particle = Instantiate(particleEffect, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
            Destroy(particle, particleLifetime);
        }
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, bombRange);
        foreach (Collider2D collision in collisions)
        {
            if (type == BulletType.Bomb)
            {
                if(collision.gameObject.CompareTag("Breakable"))
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    collision.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                }
                if(collision.gameObject.CompareTag("Destructible"))
                    Destroy(collision.gameObject);
                if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
                    UniversalTakeDamage(collision.gameObject, damage);
            }
            else if (type == BulletType.Impulse)
            {
                Vector2 point = collision.ClosestPoint(gameObject.transform.position);
                Vector2 direction = new Vector2(point.x - transform.position.x, point.y - transform.position.y);
                direction = direction.normalized;
                if (direction.x < .5f && direction.y < .5f)
                {
                    direction.x += (1 - direction.x);
                    direction.y += (1 - direction.y);
                }
                else if(direction.x > .5f && direction.y > .5f)
                {
                    direction.x -= (1 + direction.x);
                    direction.y -= (1 + direction.y);
                }
                if(collision.gameObject.layer != 8)
                        collision.GetComponent<Rigidbody2D>().AddForce(direction * blastPower, ForceMode2D.Impulse);
            }
        }
        Destroy(gameObject);
    }
    #endregion
    #region UNIVERSAL TAKE DAMAGE FUNCTION
    public static void UniversalTakeDamage(GameObject collision, float damage)
    {
        try
        { 
            var target = collision.GetComponent<Player>();
            target.heal = false;
            target.currentHealth -= damage;
        }
        catch 
        { 
            try 
            { 
                var target = collision.GetComponent<Enemy>();
                target.currentHealth -= damage;
                target.shotAt = true;
            }
            catch 
                { Debug.LogError("Player or Enemy Component not found!"); }
        }
    }
    #endregion
}
