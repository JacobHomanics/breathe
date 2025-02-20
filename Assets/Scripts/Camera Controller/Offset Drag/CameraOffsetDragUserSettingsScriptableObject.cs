using UnityEngine;

[CreateAssetMenu(fileName = "Offset Drag - User Settings", menuName = "Scriptable Objects/Camera/Offset Drag/User Settings")]
public class CameraOffsetDragUserSettingsScriptableObject : ScriptableObject
{
    public bool invertXAxis;
    public bool invertYAxis = true;

    public string xAxis = "Mouse X";
    public string yAxis = "Mouse Y";

    public Vector2 sensitivities;
}
