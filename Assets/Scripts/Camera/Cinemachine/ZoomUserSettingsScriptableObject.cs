using UnityEngine;

[CreateAssetMenu(fileName = "Zoom - User Settings", menuName = "Scriptable Objects/Camera/Components/Zoom/User Settings")]
public class ZoomUserSettingsScriptableObject : ScriptableObject
{
    public float zoom = 5f;

    public float maxZoom = 10f;
    public float sensitivity = 4f;
    public string zoomAxis = "Mouse ScrollWheel";

    public bool invertAxis;

    public float smoothTime;
}
