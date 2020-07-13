using UnityEngine;
using UnityEditor;
public class SaveDataWindow : EditorWindow
{
    public string area;
    public int bulletUnlocks;
    public int chapter;
    [MenuItem("Tools/Save Data Manually")]
    #region SHOW WINDOW FUNCTION
    public static void ShowWindow()
    {
        GetWindow<SaveDataWindow>("Save Data Manually");
    }
    #endregion
    #region ON GUI FUNCTION
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Type Safety", "UNT0010:MonoBehaviour instance creation", Justification = "<Pending>")]
    void OnGUI()
    {
        GUILayout.Label("Player Data");
        area = EditorGUILayout.TextField("Area", area);
        bulletUnlocks = EditorGUILayout.IntField("Guns And Bullets Unlocked", bulletUnlocks);
        chapter = EditorGUILayout.IntField("Chapter", chapter);
        if (GUILayout.Button("Save Player Data"))
            SaveSystem.Save(new Player { area = area, bulletUnlocks = bulletUnlocks, chapter = chapter });
        else if(GUILayout.Button("Delete Player Data"))
            SaveSystem.Delete();
    }
    #endregion
}
