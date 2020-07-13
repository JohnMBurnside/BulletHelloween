[System.Serializable]
public class PlayerData
{
    #region VARIABLES
    public int level;
    #endregion
    #region PLAYER DATA FUNCTION
    public PlayerData (Player player)
        { level = player.level; }
    #endregion
}
