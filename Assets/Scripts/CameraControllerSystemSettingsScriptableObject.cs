using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerSystemSettingsScriptableObject", menuName = "Scriptable Objects/Camera/CameraControllerSystemSettingsScriptableObject")]
public class CameraControllerSystemSettingsScriptableObject : ScriptableObject
{
    public enum XRotationMethod { EulerAngles, AngleAxis }
    public XRotationMethod xRotationMethod;

    public Vector2 clamps = new(-80, 80);
}
