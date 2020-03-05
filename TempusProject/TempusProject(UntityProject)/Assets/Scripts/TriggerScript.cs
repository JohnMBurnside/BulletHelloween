using UnityEngine;
using UnityEngine.SceneManagement;
using Tempus;
public class TriggerScript : MonoBehaviour
{
    #region VARIABLES
    public TriggerEnum TriggerTpye;
    public SceneListEnum SceneToTransition;
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (TriggerTpye)
        {
            case TriggerEnum.SceneToScene:
                if (collision.gameObject.CompareTag("Player"))
                    SceneManager.LoadScene(SceneToTransition.ToString());
                break;
        }
    }
    #endregion
}
