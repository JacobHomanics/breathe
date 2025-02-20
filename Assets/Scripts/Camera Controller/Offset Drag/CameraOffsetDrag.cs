using UnityEngine;

public class CameraOffsetDrag : MonoBehaviour
{
    public Transform target;
    public CameraOffsetDragUserSettingsScriptableObject userSettings;
    public CameraOffsetDragSystemSettingsScriptableObject systemSettings;

    public Transform character;

    public PlayerMotor motor;

    void Update()
    {
        Calculate(target);
    }

    private void Calculate(Transform target)
    {
        var isDragEnabled = userSettings.combo.IsResolved;

        if (motor.IsForwardActivated || motor.IsBackwardActivated)
        {
            if (!isDragEnabled)
                LerpToDefaultEulerAngles(target);
        }

        if (isDragEnabled)
            Drag(target, userSettings.sensitivities, userSettings.xAxis, userSettings.yAxis, userSettings.invertXAxis, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);
    }

    public void LerpToDefaultEulerAngles(Transform target)
    {
        var targetEulers = target.eulerAngles;

        if (target.rotation.y != Quaternion.Euler(userSettings.defaultEulerAngles).y)
        {
            targetEulers.y = userSettings.defaultEulerAngles.y;
        }

        //Set to != to work in all situations.
        //This does not match WoW's functionality, where if the axis is more than the default rotation,
        //then it is true.
        //I.E. if the camera is rotated below the player, then this should not be true
        if (target.rotation.x != Quaternion.Euler(userSettings.defaultEulerAngles).x)
        {
            targetEulers.x = userSettings.defaultEulerAngles.x;
        }

        var targetRot = character.rotation * Quaternion.Euler(targetEulers);

        LerpToEulerAngles(target, targetRot, userSettings.defaultSpeed);
    }


    private void LerpToEulerAngles(Transform transform, Quaternion targetRot, float speed)
    {
        var result = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * speed);
        transform.rotation = result;
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
}
