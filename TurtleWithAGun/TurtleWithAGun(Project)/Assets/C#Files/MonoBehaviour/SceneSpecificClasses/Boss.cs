using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    #region VARIABLES
    [Header("Boss Settings")]
    public EdgeCollider2D enterAndExit;
    public GameObject[] gameObjectsToDestory;
    public Slider slider;
    public Enemy enemyClass;
    public GameObject gun;
    public int bossID;
    #endregion
    #region START FUNCTION
    void Start()
    {
        PlayerData data = SaveSystem.Load();
        if (bossID != data.bulletUnlocks)
        {
            Destroy(gameObject);
            Destroy(enterAndExit);
            foreach(GameObject gameObjects in gameObjectsToDestory)
                Destroy(gameObjects);
        }
        enemyClass = gameObject.GetComponent<Enemy>();
        slider.maxValue = enemyClass.currentHealth;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        slider.value = enemyClass.currentHealth;
        if (enemyClass.currentHealth < 0 || enemyClass == null)
        {
            slider.gameObject.SetActive(false);
            enterAndExit.enabled = false;
            gun.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
    #endregion
}
