using Unity.Cinemachine;
using UnityEngine;

public class CinemachineThirdPersonFollowScrollWheelZoom : MonoBehaviour
{
    public CinemachineThirdPersonFollow thirdPersonFollow;

    public float sensitivity = 100f;
    // public float lerpSpeed = 100f;

    private void HandleZoom()
    {
        bool invertZoomAxis = false;

        var delta = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        if (invertZoomAxis)
            delta = -delta;

        float targetDistance = thirdPersonFollow.CameraDistance + delta;
        targetDistance = Mathf.Clamp(targetDistance, 0, 10);
        thirdPersonFollow.CameraDistance = targetDistance;//Mathf.Lerp(follow.CameraDistance, targetDistance, Time.deltaTime * lerpSpeed);
    }

    void Update()
    {
        HandleZoom();
    }
}
