#region NAMESPACES
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#endregion
public class PlayerCombat : MonoBehaviour
{
    #region VARIABLES
    [Header("Weapon Settings")]
    public Weapon weapon;
    public int attackDamage;
    [Header("Melee Range Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    [Header("Ranged Attack Settings")]
    public Transform firePoint;
    public GameObject arrowPrefab;
    public float arrowsLeft = 10;
    public Text arrowsLeftText;
    [Header("Delay Settings")]
    public float attackDelay = 2;
    public float attackTimer;
    [Header("Arrow Settings")]
    public float speed = 10f;
    [Header("Animation Settings")]
    Animator animator;
    public float attackAnimationDuration;
    public float shootAnimationDuration;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        animator = GetComponent<Animator>();
        arrowsLeftText.text = "x" + arrowsLeft;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackDelay && Input.GetKeyDown(KeyCode.Mouse0))
                StartCoroutine(Attack());
        if (SceneManager.GetActiveScene().name.StartsWith("L") && weapon.weaponType == Weapon.Weapons.sword)
            animator.SetBool("Sword", true);
        else
            animator.SetBool("Sword", false);
        attackDamage = weapon.damage;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            weapon.weaponType = weapon.weaponType = Weapon.Weapons.bow;
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            weapon.weaponType = weapon.weaponType = Weapon.Weapons.sword;
    }
    #endregion
    #region ON DRAW GIZMO SELECTED FUNCTION
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        else
            return;
    }
    #endregion
    //COMBAT FUNCTIONS
    #region ATTACK FUNCTION
    IEnumerator Attack()
    {
        //Melee Attack
        if (weapon.weaponType == Weapon.Weapons.sword)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(attackAnimationDuration);
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in enemiesHit)
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            attackTimer = 0;
        }
        //Shoot
        if (weapon.weaponType == Weapon.Weapons.bow && arrowsLeft > 0)
        {
            attackTimer = 0;
            animator.SetTrigger("Shoot");
            yield return new WaitForSeconds(shootAnimationDuration);
            arrowsLeft--;
            arrowsLeftText.text = "x" + arrowsLeft;
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            Vector2 shootDir;
            if(GetComponent<PlayerMovement>().facingRight)
                shootDir = new Vector2(1, 0);
            else
                shootDir = new Vector2(-1, 0);
            arrow.GetComponent<Rigidbody2D>().velocity = shootDir * speed;
            if (GetComponent<PlayerMovement>().facingRight == false)
            {
                arrow.AddComponent<ArrowScript>().flip = true;
                arrow.GetComponent<ArrowScript>().attackDamage = attackDamage;
            }
            else
                arrow.AddComponent<ArrowScript>().attackDamage = attackDamage;
        }
    }
    #endregion
}