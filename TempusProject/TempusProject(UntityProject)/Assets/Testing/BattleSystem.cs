using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#region BATTLE STATE ENUM
public enum BattleState 
{ 
    BattleSetUp, 
    PlayerTurn, 
    EnemyTurn, 
    Won, 
    Lost 
}
#endregion
public class BattleSystem : MonoBehaviour
{
    #region VARIABLES
    [Header("Battle Settings")]
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBattleLoc;
    public Transform enemyBattleLoc;
    public Text dialougeText;
    [Header("HUD Settings")]
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    Unit playerUnit;
    Unit enemyUnit;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        state = BattleState.BattleSetUp;
        StartCoroutine(BattleSetUp());
    }
    #endregion
    //BATTLE SYSTEM FUNCTIONS
    #region BATTLE SET UP FUNCTION
    IEnumerator BattleSetUp()
    {
        GameObject player = Instantiate(playerPrefab, playerBattleLoc);
        playerUnit = player.GetComponent<Unit>();
        GameObject enemy = Instantiate(enemyPrefab, enemyBattleLoc);
        enemyUnit = enemy.GetComponent<Unit>();
        dialougeText.text = "A wild " + enemyUnit.unitName + " approaches...";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        yield return new WaitForSeconds(2f);
        state = BattleState.PlayerTurn;
        PlayerTurn();
    }
    #endregion
    #region PLAYER TURN FUNCTION
    void PlayerTurn()
    {
        dialougeText.text = "Choose an attack";
    }
    #endregion
    #region PLAYER ATTACK FUNCTION
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHp);
        dialougeText.text = "The attack was a success!";
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            state = BattleState.Won;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }
    #endregion
    #region ON ATTACK BUTTON FUNCTION
    public void OnAttackButton()
    {
        if (state != BattleState.PlayerTurn)
            return;
        StartCoroutine(PlayerAttack());
    }
    #endregion
    #region ENEMY TURN FUNCTION
    IEnumerator EnemyTurn()
    {
        dialougeText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHp);
        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }
    #endregion
    #region END BATTLE FUNCTION
    void EndBattle()
    {
        if (state == BattleState.Won)
            dialougeText.text = "You Won!";
        else if (state == BattleState.Lost)
            dialougeText.text = "You blacked out!";
    }
    #endregion
}
