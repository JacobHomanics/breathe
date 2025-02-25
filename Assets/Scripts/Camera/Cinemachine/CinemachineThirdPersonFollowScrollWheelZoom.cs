using Unity.Cinemachine;
using UnityEngine;

public class CinemachineThirdPersonFollowScrollWheelZoom : MonoBehaviour
{
    public CinemachineThirdPersonFollow thirdPersonFollow;

    public ZoomSystemSettingsScriptableObject systemSettings;
    public ZoomUserSettingsScriptableObject userSettings;

    public float Zoom { get; private set; }
    private float velocity;

    void Start()
    {
        Zoom = userSettings.zoom;
        thirdPersonFollow.CameraDistance = Zoom;
    }

    void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis(userSettings.zoomAxis);
        scroll = userSettings.invertAxis ? -scroll : scroll;
        Zoom += scroll * userSettings.sensitivity;
        Zoom = Mathf.Clamp(Zoom, systemSettings.minZoom, 10);
        thirdPersonFollow.CameraDistance = Mathf.SmoothDamp(thirdPersonFollow.CameraDistance, Zoom, ref velocity, userSettings.smoothTime);
    }
}
