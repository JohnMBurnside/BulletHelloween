#region NAMESPACES
using UnityEngine;
#endregion
public class ArrowScript : MonoBehaviour
{
    #region VARIABLES
    public int attackDamage;
    float timer = 0;
    [HideInInspector] public bool flip;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        if(flip == true)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
            Destroy(gameObject);
    }
    #endregion
    #region ON COLLISION ENTER 2D FUNCTION
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyHealth>() != null)
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        Destroy(gameObject); 
    }
    #endregion
}