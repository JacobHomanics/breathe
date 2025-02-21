using UnityEngine;

[CreateAssetMenu(fileName = "Offset Drag - System Settings", menuName = "Scriptable Objects/Camera/Offset Drag/System Settings")]
public class CameraOffsetDragSystemSettingsScriptableObject : ScriptableObject
{
    public enum XRotationMethod { EulerAngles, AngleAxis }
    public XRotationMethod xRotationMethod;

    public Vector2 clamps = new(-80, 80);

    public float cursorHideThresholdOnDrag = 100f;
}
