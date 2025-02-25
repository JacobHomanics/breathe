using Unity.Cinemachine;
using UnityEngine;

public class CinemachineThirdPersonFollowScrollWheelZoom : MonoBehaviour
{
    public CinemachineThirdPersonFollow thirdPersonFollow;

    public float sensitivity = 4f;

    public float smoothTime = 0.25f;

    public bool invertAxis;

    public float Zoom { get; private set; }
    private float velocity;

    void Start()
    {
        Zoom = thirdPersonFollow.CameraDistance;
    }

    void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        scroll = invertAxis ? -scroll : scroll;
        Zoom += scroll * sensitivity;
        Zoom = Mathf.Clamp(Zoom, 0, 10);
        thirdPersonFollow.CameraDistance = Mathf.SmoothDamp(thirdPersonFollow.CameraDistance, Zoom, ref velocity, smoothTime);
    }
}
