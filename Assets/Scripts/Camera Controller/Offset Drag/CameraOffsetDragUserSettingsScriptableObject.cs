using UnityEngine;

[CreateAssetMenu(fileName = "Offset Drag - User Settings", menuName = "Scriptable Objects/Camera/Offset Drag/User Settings")]
public class CameraOffsetDragUserSettingsScriptableObject : ScriptableObject
{
    public bool invertYAxis = true;

    public Vector2 sensitivities;
}
