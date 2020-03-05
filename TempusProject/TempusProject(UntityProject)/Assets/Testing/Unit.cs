using UnityEngine;
public class Unit : MonoBehaviour
{
    #region VARIABLES 
    [Header("Specification Settings")]
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHp;
    #endregion
    //UNIT FUNCTIONS
    #region TAKE DAMAGE FUNCTION
    public bool TakeDamage(int dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0)
            return true;
        else
            return false;
    }
    #endregion
}
