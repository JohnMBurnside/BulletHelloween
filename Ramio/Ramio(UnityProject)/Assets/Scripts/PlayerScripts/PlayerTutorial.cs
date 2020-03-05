#region NAMESPACES
using UnityEngine;
using UnityEngine.UI;
#endregion
public class PlayerTutorial : MonoBehaviour
{
    #region VARIABLES
    [Header("Text Settings")]
    public Text[] tips = new Text[4];
    int arrayNum = 5;
    public float timer;
    [Header("UI Elements")]
    public GameObject playerLoadOut;
    public GameObject slowMoSlider;
    bool startTime;
    #endregion
    //UNITY FUNCTION
    #region START FUNCTION
    void Start()
    {
        for(int i = 0; i < 4; i++)
            tips[i].gameObject.SetActive(false);
        playerLoadOut.gameObject.SetActive(false);
        slowMoSlider.gameObject.SetActive(false);
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        switch(arrayNum)
        {
            case 0:
                tips[arrayNum].gameObject.SetActive(true);
                if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || startTime == true)
                {
                    startTime = true;
                    timer += Time.deltaTime;
                    if(timer > 1f)
                    {
                        arrayNum = 5;
                        startTime = false;
                        timer = 0;
                    }
                }
                break;
            case 1:
                tips[arrayNum].gameObject.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Space))
                    arrayNum = 5;
                break;
            case 2:
                tips[arrayNum].gameObject.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Mouse0) || startTime == true)
                {
                    startTime = true;
                    timer += Time.deltaTime;
                    if(timer > 1f)
                    {
                        arrayNum = 5;
                        startTime = false;
                        timer = 0;
                    }
                }
                break;
            case 3:
                tips[arrayNum].gameObject.SetActive(true);
                playerLoadOut.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Alpha2) || startTime == true)
                {
                    startTime = true;
                    timer += Time.deltaTime;
                    if(timer > 1f)
                    {
                        arrayNum = 5;
                        startTime = false;
                        timer = 0;
                    }
                }
                break;
            case 4:
                tips[arrayNum].gameObject.SetActive(true);
                slowMoSlider.SetActive(true);
                if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || startTime == true)
                {
                    startTime = true;
                    timer += Time.deltaTime;
                    if(timer > 1f)
                    {
                        arrayNum = 5;
                        startTime = false;
                        timer = 0;
                    }
                }
                break;
            case 5:
                tips[0].gameObject.SetActive(false);
                tips[1].gameObject.SetActive(false);
                tips[2].gameObject.SetActive(false);
                tips[3].gameObject.SetActive(false);
                tips[4].gameObject.SetActive(false);
                break;            
        }
    }
    #endregion
    #region ON TRIGGER ENTER 2D FUNCTION
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.name)
        {
            case "MovingTipTrigger":
                arrayNum = 0;
                break;
            case "JumpingTipTrigger":
                arrayNum = 1;
                break;
            case "AttackTipTrigger":
                arrayNum = 2;
                break;
            case "SwitchingWeaponsTipTrigger":
                arrayNum = 3;
                break;
            case "SlowMoTipTrigger":
                arrayNum = 4;
                break;
        }
    }
    #endregion
}
