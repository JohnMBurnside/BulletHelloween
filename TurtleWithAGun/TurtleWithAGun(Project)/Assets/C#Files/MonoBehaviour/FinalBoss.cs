using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FinalBoss : MonoBehaviour
{
    #region VARIABLES
    public GameObject deathParticleEffect;
    public Slider slider;
    public Enemy enemyClass;
    public GameObject destroy;
    public UI fadeUI;
    public bool kill;
    #region START FUNCTION
    #endregion
    void Start()
    {
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
            if(!kill)
                StartCoroutine(Death());
        }
    }
    #endregion
    #region DEATH FUNCTION
    IEnumerator Death()
    {
        kill = true;
        Instantiate(deathParticleEffect, gameObject.transform);
        Destroy(destroy);
        yield return new WaitForSeconds(5f);
        StartCoroutine(fadeUI.FadeOut("ThanksForPlaying", false, 0));
    }
    #endregion
}
