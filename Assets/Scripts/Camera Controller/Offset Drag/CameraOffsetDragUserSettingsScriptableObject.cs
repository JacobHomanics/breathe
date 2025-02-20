using UnityEngine;

[CreateAssetMenu(fileName = "CameraOffsetDragUserSettingsScriptableObject", menuName = "Scriptable Objects/Camera/CameraOffsetDragUserSettingsScriptableObject")]
public class CameraOffsetDragUserSettingsScriptableObject : ScriptableObject
{
    public bool invertYAxis = true;

    public Vector2 sensitivities;
}
