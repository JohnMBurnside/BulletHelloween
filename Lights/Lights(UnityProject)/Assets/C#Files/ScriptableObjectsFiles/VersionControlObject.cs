#region NAMESPACES
using System;
using UnityEngine;
#endregion
[CreateAssetMenu(fileName = "VersionData", menuName = "Version")]
public class VersionControlObject : ScriptableObject
{
    #region VARIABLES
    public string version = "1.0.0";
    public VersionPhases versionPhase = VersionPhases.Development;
    [HideInInspector] public string phase;
    #endregion
    //UNITY FUNCTIONS
    #region AWAKE FUNCTION
    void Awake(){ phase = Enum.GetName(typeof(VersionPhases), versionPhase); }
    #endregion
}
