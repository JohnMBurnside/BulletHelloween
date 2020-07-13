using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class SaveSystem
{
    public static string path = Application.persistentDataPath + "/SaveData.twag";
    #region SAVE FUNCION
    public static void Save(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    #endregion
    #region LOAD FUNCTION
    public static PlayerData Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
            return null;
    }
    #endregion
    #region DELETE FUNCTION
    public static void Delete()        
    { 
        File.Delete(path);
        PlayerPrefs.DeleteAll();
    }
    #endregion
}
