using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerUserSettingsScriptableObject", menuName = "Scriptable Objects/Camera/CameraControllerUserSettingsScriptableObject")]
public class CameraControllerUserSettingsScriptableObject : ScriptableObject
{
    // WoW style config:


    [Header("Zoom settings")]
    public float zoom = 5f;

    public float maxZoom = 10f;
    public float zoomSensitivity = 90f;
    public string zoomAxis = "Mouse ScrollWheel";

    public bool invertZoomAxis;


    [Header("Bob")]

    public Vector3 periods = new(0, 0.5f, 0);
    public Vector3 amplitudes = new(0, 0.2f, 0);

    [Header("Other")]
    public float smoothing = 1f;
    public bool invertYAxis;

    public Vector2 sensitivities;


}
