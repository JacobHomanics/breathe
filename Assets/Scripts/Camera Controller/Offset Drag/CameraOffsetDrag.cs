using UnityEngine;

public class CameraOffsetDrag : MonoBehaviour
{
    public Transform target;
    public CameraOffsetDragUserSettingsScriptableObject userSettings;
    public CameraOffsetDragSystemSettingsScriptableObject systemSettings;


    void Update()
    {
        Calculate(target);
    }

    private void Calculate(Transform target)
    {
        if (Input.GetMouseButton(0))
            Drag(target, userSettings.sensitivities, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);
    }

    private void Drag(Transform target, Vector2 sensitivities, bool invertYAxis, CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod xRotationMethod, Vector2 clamps)
    {
        var yFrame = GetY(invertYAxis) * sensitivities.y * Time.deltaTime;
        var xFrame = Input.GetAxis("Mouse X") * sensitivities.x * Time.deltaTime;

        target.rotation = Quaternion.AngleAxis(xFrame, Vector3.up) * target.rotation;

        if (xRotationMethod == CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod.AngleAxis)
            target.rotation = Quaternion.AngleAxis(yFrame, target.right) * target.rotation;

        var ea = target.rotation.eulerAngles;

        if (xRotationMethod == CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod.EulerAngles)
            ea.x += yFrame;

        ea.x = ClampAngle(ea.x, clamps.x, clamps.y);
        target.eulerAngles = ea;
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }


    private float GetY(bool invert)
    {
        float y;

        if (invert)
        {
            y = -Input.GetAxis("Mouse Y");
        }
        else
        {
            y = Input.GetAxis("Mouse Y");
        }

        return y;
    }
}
