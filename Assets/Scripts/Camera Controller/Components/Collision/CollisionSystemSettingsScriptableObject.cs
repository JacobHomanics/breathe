using UnityEngine;

[CreateAssetMenu(fileName = "Collision - System Settings", menuName = "Scriptable Objects/Camera/Components/Collision - System Settings")]
public class CollisionSystemSettingsScriptableObject : ScriptableObject
{
    public LayerMask layerMask;
}
