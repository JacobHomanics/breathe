using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerSystemSettingsScriptableObject", menuName = "Scriptable Objects/Camera/CameraControllerSystemSettingsScriptableObject")]
public class CameraControllerSystemSettingsScriptableObject : ScriptableObject
{
    // WoW style config:

    public float minZoom = 0f;

    public LayerMask layerMask;


    public enum XRotationMethod { EulerAngles, AngleAxis }
    public XRotationMethod xRotationMethod;

    public Vector2 clamps = new(-80, 80);
}
