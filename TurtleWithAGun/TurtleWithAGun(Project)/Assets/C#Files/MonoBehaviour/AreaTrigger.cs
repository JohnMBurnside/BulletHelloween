using UnityEngine;
using UnityEngine.UI;
public class AreaTrigger : MonoBehaviour
{
    #region VARIABLES
    [Header("Area Trigger Settings")]
    public string areaToLoad;
    public int triggerIndexToLoad;
    public bool goRight;
    [Header("Boss Settings")]
    public bool bossTrigger;
    #pragma warning disable CS0108
    public Camera camera;
    #pragma warning restore CS0108
    public Slider bossSlider;
    public EdgeCollider2D enterAndExit;
    public bool bossOn;

    public bool final;
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !bossTrigger)
        {
            Player player = collision.GetComponent<Player>();
            player.inputOn = false;
            if (goRight)
                player.moveX = 1;
            else
                player.moveX = -1;
            StartCoroutine(player.fadeUI.FadeOut(areaToLoad, true, triggerIndexToLoad));
        }
        else if(bossTrigger && !bossOn)
        {
            bossOn = true;
            collision.GetComponent<Player>().playerCamera.enabled = false;
            camera.enabled = true;
            bossSlider.gameObject.SetActive(true);
            if (final)
            {
                PlayerPrefs.SetInt("triggerIndex", 1);
                collision.GetComponent<Rigidbody2D>().gravityScale = .3f;
            }
            else
                enterAndExit.enabled = true;
        }
    }
    #endregion
}
