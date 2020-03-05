#region NAMESPACES
using UnityEngine;
#endregion
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    #region VARIABLES
    public enum Weapons {sword, bow}
    public Weapons      weaponType;
    public string       weaponName;
    public int          damage;
    #endregion
}
