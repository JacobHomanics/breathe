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

    private void Calculate()
    {
        Vector3 targetPosition = Vector3.zero;

        for (int i = 0; i < components.Length; i++)
        {
            targetPosition = components[i].Calculate(targetPosition);
        }

        cam.localPosition = Vector3.Lerp(cam.localPosition, targetPosition, Time.deltaTime * userSettings.smoothing);
    }
}
