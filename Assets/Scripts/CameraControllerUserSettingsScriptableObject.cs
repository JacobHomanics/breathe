using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerUserSettingsScriptableObject", menuName = "Scriptable Objects/Camera/CameraControllerUserSettingsScriptableObject")]
public class CameraControllerUserSettingsScriptableObject : ScriptableObject
{
    public float smoothing = 1f;
    public bool invertYAxis;

    public Vector2 sensitivities;


}
