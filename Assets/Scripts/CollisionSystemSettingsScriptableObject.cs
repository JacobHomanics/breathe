using UnityEngine;

[CreateAssetMenu(fileName = "CollisionSystemSettingsScriptableObject", menuName = "Scriptable Objects/Camera/CollisionSystemSettingsScriptableObject")]
public class CollisionSystemSettingsScriptableObject : ScriptableObject
{
    public LayerMask layerMask;
}
