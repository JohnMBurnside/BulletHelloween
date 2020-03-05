using UnityEngine;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{
    #region VARIABLES
    public Text nameText;
    public Text levelText;
    public Slider hpBar;
    #endregion
    //BATTLE HUD FUNCTIONS
    #region SETHUD FUNCTION
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lv. " + unit.unitLevel;
        hpBar.maxValue = unit.maxHP;
        hpBar.value = unit.maxHP;
    }
    #endregion
    #region SET HP FUNCTION
    public void SetHP(int hp)
    {
        hpBar.value = hp;
    }
    #endregion
}
