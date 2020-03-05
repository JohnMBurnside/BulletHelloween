using UnityEngine;
public class BulletScript : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]           
    public int speedRotate = 50;
    public bool spin;
    public bool bat;
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if(spin == false)
            transform.right = GetComponent<Rigidbody2D>().velocity;
        else
            transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
        if (bat == true && GetComponent<Rigidbody2D>().velocity.x < 0)
            GetComponent<SpriteRenderer>().flipY = true;
        else
            GetComponent<SpriteRenderer>().flipY = false;
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision) { Destroy(gameObject); }
    #endregion
}