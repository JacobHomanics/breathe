using UnityEngine;

[CreateAssetMenu(fileName = "Zoom - User Settings", menuName = "Scriptable Objects/Camera/Components/Zoom/User Settings")]
public class ZoomUserSettingsScriptableObject : ScriptableObject
{
    public float zoom = 5f;

    public float maxZoom = 10f;
    public float zoomSensitivity = 90f;
    public string zoomAxis = "Mouse ScrollWheel";

    public bool invertZoomAxis;
}
