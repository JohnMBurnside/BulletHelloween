#region NAMESPACES
using UnityEngine;
using UnityEditor;
#endregion
[CreateAssetMenu(fileName = "SaveData", menuName = "Save Data")]
public class SaveDataObject : ScriptableObject
{
    #region VARIABLES
    [Header("Player Data")]
    public int level;
    #endregion
    //SAVE DATA FUNCTIONS
    #region SAVE DATA FUNCTION
    public void SaveData()
    {
        Player data = new Player();
        data.level = level;
        SaveSystem.Save(data);
    }
    #endregion
}
[CustomEditor(typeof(SaveDataObject))]
public class SaveDataEditor : Editor
{
    //UNITY FUNCTIONS
    #region ON INSPECTOR GUI FUNCTION
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SaveDataObject saveData = (SaveDataObject)target;
        if (GUILayout.Button("Save"))
            saveData.SaveData();
    }
    #endregion
}