using UnityEngine;

public class Zoom : CameraControllerComponent
{
    public Transform centerPoint;

    public ZoomUserSettingsScriptableObject userSettings;
    public ZoomSystemSettingsScriptableObject systemSettings;

    public float Current { get; private set; }

    void Start()
    {
        Current = userSettings.zoom;
    }

    public override Vector3 Calculate(Vector3 point)
    {
        return centerPoint.TransformPoint(new Vector3(0, 0, -HandleZoom()));
    }

    private float HandleZoom()
    {
        if (userSettings.invertZoomAxis)
            Current -= Input.GetAxis(userSettings.zoomAxis) * userSettings.zoomSensitivity * Time.deltaTime;
        else
            Current += Input.GetAxis(userSettings.zoomAxis) * userSettings.zoomSensitivity * Time.deltaTime;

        if (Current > userSettings.maxZoom)
            Current = userSettings.maxZoom;

        if (Current < systemSettings.minZoom)
            Current = systemSettings.minZoom;

        return Current;
    }
}
