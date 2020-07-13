[System.Serializable]
public class PlayerData
{
    #region VARIABLES
    public string area;                 // What area the player saved in
    public int bulletUnlocks;           // How much bullets the player has unlocked
    public int chapter;                 // How far the player has progressed
    #endregion
    #region PLAYER DATA CONSTRUCTOR
    public PlayerData(Player player)
    {
        area = player.area;
        bulletUnlocks = player.bulletUnlocks;
        chapter = player.chapter;
    }
    #endregion
}
