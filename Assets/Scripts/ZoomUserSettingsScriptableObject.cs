using UnityEngine;

[CreateAssetMenu(fileName = "ZoomUserSettingsScriptableObject", menuName = "Scriptable Objects/Camera/ZoomUserSettingsScriptableObject")]
public class ZoomUserSettingsScriptableObject : ScriptableObject
{
    public float zoom = 5f;

    public float maxZoom = 10f;
    public float zoomSensitivity = 90f;
    public string zoomAxis = "Mouse ScrollWheel";

    public bool invertZoomAxis;
}
