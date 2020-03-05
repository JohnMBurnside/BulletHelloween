using UnityEngine;
using UnityEngine.UI;
using Tempus;
public class BattleScript : MonoBehaviour
{
    #region VARIABLES
    [Header("Battle Settings")]
    public Vector3 playerBattleLocation;
    public Vector3 enemyBattleLocation;
    public GameObject battleCanvas;
    Vector3 prevPlayerPos = new Vector3(0,0,0);
    bool OnBattleStart = true;
    bool OnBattleComplete;
    public GameObject player;
    public Text playerHealthText;
    public Text enemyHealthText;
    public int playerHealth = 0;
    public GameObject attackButtons;
    GameObject[] attackMoves = new GameObject[5];
    int maxHealth;
    int prevHealth;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        //Set UI elements
        battleCanvas = GameObject.Find("UI/BattleCanvas");
        battleCanvas.GetComponent<Canvas>().enabled = false;
        //Set player variables
        player = GameObject.FindGameObjectWithTag("Player");
        //Disable battle camera
        GetComponent<Camera>().enabled = false;
        //Set Attack Options as false
        attackButtons = GameObject.Find("UI/BattleCanvas/PlayerBox/AttackButtons");
        for (int i = 0; i < attackButtons.transform.childCount; i++)
        {
            attackMoves[i] = GameObject.Find("AttackOption" + i);
            attackMoves[i].SetActive(false);
        }
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnBattleComplete = true;
        if (player.GetComponent<PlayerScript>().enemyData.battleOn == true)
            BattleOn();
        else
            OnBattleStart = true;
    }
    #endregion
    //BATTLE FUNCTIONS
    #region BATTLE ON FUNCTION
    void BattleOn()
    {
        #region LOCAL VARIABLES
        //PLAYER VARIABLES
        int playerAttack = 0;
        int playerDefense = 0;
        float playerSpeed = 0;
        //ENEMY VARIABLES
        GameObject enemy = null;
        int enemyHealth = 20;
        int enemyAttack = 0;
        int enemyDefense = 0;
        float enemySpeed = 0;
        //UI VARIABLES
        Text playerLevelText;
        #endregion
        #region ON BATTLE START
        if (OnBattleStart)
        {
            //Defining new local variables
            enemy = player.GetComponent<PlayerScript>().enemyData.enemy;
            string enemyType = player.GetComponent<PlayerScript>().enemyData.enemyType;
            int enemyLevel = player.GetComponent<PlayerScript>().enemyData.enemyLevel;
            //Set up for player stats
            playerHealth = Stats.HealthCalc(player.GetComponent<PlayerScript>().level);
            playerAttack = Stats.AttackCalc(player.GetComponent<PlayerScript>().level);
            playerDefense = Stats.DefenseCalc(player.GetComponent<PlayerScript>().level);
            playerSpeed = Stats.SpeedCalc(player.GetComponent<PlayerScript>().tempusLevel);
            //Get previous player location, so the player will teleport back to his position after the battle
            Vector3 prevPlayerPos = player.GetComponent<Transform>().position;
            //Teleport the player and enemy to their battle positions
            player.GetComponent<Transform>().position = playerBattleLocation;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            enemy.GetComponent<Transform>().position = enemyBattleLocation;
            enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //Enable the battle camera, disable the player camera
            player.GetComponentInChildren<Camera>().enabled = false;
            GetComponent<Camera>().enabled = true;
            //Enable UI battle elements
            playerLevelText = GameObject.Find("UI/BattleCanvas/PlayerBox/InfoText/LevelText").GetComponent<Text>();
            playerLevelText.GetComponent<Text>().text = "Lv. " + player.GetComponent<PlayerScript>().level;
            playerHealthText = GameObject.Find("UI/BattleCanvas/PlayerBox/InfoText/HealthText").GetComponent<Text>();
            battleCanvas.GetComponent<Canvas>().enabled = true;
            playerHealthText.text = playerHealth + "/" + playerHealth;
            maxHealth = playerHealth;
            prevHealth = playerHealth;
            //When On Battle Start is finished excute this code to exit out of the statement
            print("OnBattleStart Complete");
            OnBattleStart = false;
        }
        #endregion
        #region DURING BATTLE
        else if (OnBattleStart == false && OnBattleComplete == false)
        {
            //Defining new local variables
            //If the players health changing during battle change the player health text
            if (prevHealth != playerHealth)
            {
                playerHealthText.text = playerHealth + "/" + maxHealth;
                prevHealth = playerHealth;
            }
            //If the enemy's health is less than 0(death) then exit out of this statement and proceed to On Battle Complete
            if(enemyHealth < 1)
                OnBattleComplete = true;
        }
        #endregion
        #region ON BATTLE COMPLETE
        else if (OnBattleComplete)
        {
            //Teleport the player back to its original position
            player.GetComponent<Transform>().position = prevPlayerPos;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //Enable the player camera, disable the battle camera
            player.GetComponentInChildren<Camera>().enabled = true;
            GetComponent<Camera>().enabled = false;
            //Disable UI battle elements
            battleCanvas.GetComponent<Canvas>().enabled = false;
            //Destroy the enemy
            Destroy(enemy);
            //When On Battle Complete is finshed 
            print("OnBattleComplete Complete");
            OnBattleComplete = false;
            player.GetComponent<PlayerScript>().enemyData.battleOn = false;
        }
        #endregion
    }
    #endregion
    #region ATTACK FUNCTION
    public void Attack()
    {
        GameObject attackButtons = GameObject.Find("UI/BattleCanvas/PlayerBox/AttackButtons");
        for (int i = 0; i < attackButtons.transform.childCount; i++)
        {
            attackMoves[i] = GameObject.Find("AttackOption" + i);
            attackMoves[i].SetActive(true);
        }
    }
    #endregion
    #region RUN FUNCTION
    public void Run()
    {
        playerHealth--;
    }
    #endregion
    #region SHOW STATS FUNCTION
    public void ShowStats()
    {
        //Show status
    }
    #endregion
}