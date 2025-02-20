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
            Drag(target, userSettings.sensitivities, userSettings.xAxis, userSettings.yAxis, userSettings.invertXAxis, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);
    }

    private void Drag(Transform target, Vector2 sensitivities, string xAxis, string yAxis, bool invertXAxis, bool invertYAxis, CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod xRotationMethod, Vector2 clamps)
    {
        var x = Input.GetAxis(xAxis);
        var y = Input.GetAxis(yAxis);


        var yDelta = invertYAxis ? -y : y * sensitivities.y * Time.deltaTime;
        var xDelta = invertXAxis ? -x : x * sensitivities.x * Time.deltaTime;

        target.rotation = Quaternion.AngleAxis(xDelta, Vector3.up) * target.rotation;

        if (xRotationMethod == CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod.AngleAxis)
            target.rotation = Quaternion.AngleAxis(yDelta, target.right) * target.rotation;

        var ea = target.rotation.eulerAngles;

        if (xRotationMethod == CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod.EulerAngles)
            ea.x += yDelta;

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
