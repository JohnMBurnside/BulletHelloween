using UnityEngine;
public class Minecart : MonoBehaviour
{
    #region VARIABLES
    public float speed;
    public bool minecartOn;
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if (minecartOn)
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }
    #endregion
}
