using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cam;
    public CameraControllerUserSettingsScriptableObject userSettings;

    void LateUpdate()
    {
        Calculate();
    }

    public CameraControllerComponent[] components;

    private Vector3 desiredPosition;

    private void Calculate()
    {
        for (int i = 0; i < components.Length; i++)
        {
            desiredPosition = components[i].Calculate(desiredPosition);
        }

        Debug.DrawLine(cam.position, desiredPosition, Color.green);
        cam.position = Vector3.Lerp(cam.position, desiredPosition, Time.deltaTime * userSettings.smoothing);
    }
}
