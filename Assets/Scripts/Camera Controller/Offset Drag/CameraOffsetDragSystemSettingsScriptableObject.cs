using UnityEngine;

[CreateAssetMenu(fileName = "CameraOffsetDragSystemSettingsScriptableObject", menuName = "Scriptable Objects/Camera/CameraOffsetDragSystemSettingsScriptableObject")]
public class CameraOffsetDragSystemSettingsScriptableObject : ScriptableObject
{
    public enum XRotationMethod { EulerAngles, AngleAxis }
    public XRotationMethod xRotationMethod;

    public Vector2 clamps = new(-80, 80);
}
