#region NAMESPACES
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
#endregion
public class SaveSystem
{
    //Creating file path to contain the save data
    public static string path = Application.persistentDataPath + "/SaveData.Lights";
    #region SAVE FUNCION
    public static void Save(Player player)
    {
        //New binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        //Creating file
        FileStream stream = new FileStream(path, FileMode.Create);
        //Creating new data
        PlayerData data = new PlayerData(player);
        //Serialize the data
        formatter.Serialize(stream, data);
        //Closing the file
        stream.Close();
    }
    #endregion
    #region LOAD FUNCTION
    public static PlayerData Load()
    {
        if (File.Exists(path))
        {
            //Creating new binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //Creating and opening new file stream
            FileStream stream = new FileStream(path, FileMode.Open);
            //Deserializing data and writing it to a new PlayerData Class, data
            PlayerData data  = formatter.Deserialize(stream) as PlayerData;
            //Closing the file
            stream.Close();
            //Returning the PlayerData class
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    #endregion
}
