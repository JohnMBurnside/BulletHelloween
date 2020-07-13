using UnityEngine;
//[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/New Gun", order = 1)]
public class Gun : ScriptableObject
{
    #region VARIABLES
    public Sprite gunSprite;
    public Vector3 position;
    public float rotation;
    public float scale;
    public GameObject bulletPrefab;
    public float damage;
    public float bulletSpeed;
    public float bulletLifetime;
    public float fireRate;
    #endregion
}
