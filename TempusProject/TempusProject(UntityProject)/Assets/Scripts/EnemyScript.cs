using UnityEngine;
using Tempus;
public class EnemyScript : MonoBehaviour
{
    #region VARIABLES
    [Header("General Settings")]
    public EnemyEnum Enemy;
    public int level = 5;
    [HideInInspector] public GameObject enemy;
    public string enemyType;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        //Setting enemy as the gameobject the script is attached to
        enemy = gameObject;
        enemyType = Enemy.ToString();
        //Setting variables based on the enemy type
        switch (Enemy)
        {
            case EnemyEnum.Minion:
                break;
            case EnemyEnum.Brute:
                break;
            case EnemyEnum.MiniBoss:
                break;
            case EnemyEnum.Boss:
                break;
            case EnemyEnum.SuperBoss:
                break;
            case EnemyEnum.HyperBoss:
                break;
            case EnemyEnum.FinalBoss:
                break;
        }
    }
    #endregion
}
