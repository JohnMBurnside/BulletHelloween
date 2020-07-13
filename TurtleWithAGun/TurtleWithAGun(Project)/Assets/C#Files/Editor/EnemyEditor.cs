using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    #region ON SCENE GUI FUNCTION
    void OnSceneGUI()
    {
        Enemy fov = (Enemy)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.right, 360, fov.viewRadius);
        Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);
        Handles.color = Color.cyan;
        if (fov.target != null)
            Handles.DrawLine(fov.gun.position, fov.target.transform.position);
    }
    #endregion
}
