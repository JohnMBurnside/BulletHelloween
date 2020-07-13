using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    #region VARIABLES
    [Header("Tutorial Settings")]
    public Player player;
    public GameObject door;
    public GameObject[] triggerPositions = new GameObject[4];
    public Vector3 triggerRange;
    public LayerMask playerLayer;
    public bool[] texts = new bool[4];
    public GameObject[] guns;
    bool buttonOnePressed;
    bool buttonTwoPressed;
    bool doorOpen;
    bool shootOn;
    [Header("*Tutorial UI Connection*")]
    public Text[] tutorialText = new Text[4];
    [Header("*Fade UI Connection*")]
    public Canvas fade;
    #endregion
    #region START FUNCTION
    void Start()
    {
        foreach (GameObject gameObject in guns)
            gameObject.SetActive(false);
        if (player == null)
            player = GameObject.Find("Bob").GetComponent<Player>();
        player.timer = -10000;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i] = Physics2D.OverlapBox(triggerPositions[i].transform.position, triggerRange, 0, playerLayer);
            if (texts[i])
                tutorialText[i].gameObject.SetActive(true);
            else
                tutorialText[i].gameObject.SetActive(false);
            if (texts[1] && !shootOn)
            {
                shootOn = true;
                foreach (GameObject gameObject in guns)
                    gameObject.SetActive(true);
                player.bulletUnlocks++;
                player.playerHUD.GetComponent<UI>().UnlockBullets(player.bulletUnlocks);
                player.timer = 0;
            }
        }
        if (buttonOnePressed && buttonTwoPressed)
            doorOpen = true;
        if (doorOpen && door.transform.position.y < 10)
            door.transform.position = new Vector3(door.transform.position.x, (door.transform.position.y + .1f), door.transform.position.z);
    }
    #endregion
    #region ON DRAW GIZMOS FUNCTION
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0);
        foreach (GameObject trigger in triggerPositions)
            Gizmos.DrawWireCube(trigger.transform.position, triggerRange);
    }
    #endregion
    #region BUTTON HIT FUNCTION
    public void ButtonHit(string button)
    {
        if (button == "ButtonOne")
        {
            buttonOnePressed = true;
            StartCoroutine(DoorOpen(1));
        }
        if (button == "ButtonTwo")
        {
            buttonTwoPressed = true;
            StartCoroutine(DoorOpen(2));
        }
    }
    #endregion
    #region DOOR OPEN FUNCTION
    public IEnumerator DoorOpen(int button)
    {
        yield return new WaitForSeconds(.5f);
        if (button == 1)
            buttonOnePressed = false;
        if (button == 2)
            buttonTwoPressed = false;
    }
    #endregion
}
