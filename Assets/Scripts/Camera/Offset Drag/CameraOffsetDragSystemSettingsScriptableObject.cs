using UnityEngine;

[CreateAssetMenu(fileName = "Offset Drag - System Settings", menuName = "Scriptable Objects/Camera/Offset Drag/System Settings")]
public class CameraOffsetDragSystemSettingsScriptableObject : ScriptableObject
{
    public Vector2 clamps = new(-80, 80);

}
