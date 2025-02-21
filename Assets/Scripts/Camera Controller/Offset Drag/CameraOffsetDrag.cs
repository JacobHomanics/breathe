using System.Security.Cryptography;
using UnityEngine;

public class CameraOffsetDrag : MonoBehaviour
{
    public Transform target;
    public Transform character;
    public PlayerMotor motor;

    public CameraOffsetDragUserSettingsScriptableObject userSettings;
    public CameraOffsetDragSystemSettingsScriptableObject systemSettings;

    public float cursorHideThresholdOnDrag = 100f;
    public Vector3 mouseStartPosOnDrag;
    public Vector3 previousMousePositionDuringDrag;

    void Start()
    {
        target.rotation = Quaternion.Euler(userSettings.defaultEulerAngles);
    }

    void Update()
    {
        Calculate(target);
    }

    public bool IsDragThresholdReached
    {
        get
        {
            var posStartAbsX = Mathf.Abs(mouseStartPosOnDrag.x);
            var posStartAbsY = Mathf.Abs(mouseStartPosOnDrag.y);

            var prevPosAbsX = Mathf.Abs(previousMousePositionDuringDrag.x);
            var prevPosAbsY = Mathf.Abs(previousMousePositionDuringDrag.y);

            var diffX = Mathf.Abs(posStartAbsX - prevPosAbsX);
            var diffY = Mathf.Abs(posStartAbsY - prevPosAbsY);


            return diffX >= cursorHideThresholdOnDrag || diffY >= cursorHideThresholdOnDrag;
        }
    }
    private void Calculate(Transform target)
    {
        if (userSettings.leftMouseButtonCombo.IsResolved || userSettings.rightMouseButtonCombo.IsResolved)
        {

        }
        else
        {
            if (motor.IsForwardActivated || motor.IsBackwardActivated)
                LerpToDefaultEulerAngles(target);
        }

        if (userSettings.leftMouseButtonCombo.IsResolved)
        {
            Drag(target, userSettings.sensitivities, userSettings.xAxis, userSettings.yAxis, userSettings.invertXAxis, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Quaternion turnAngle = Quaternion.Euler(0, target.eulerAngles.y, 0);
            character.rotation = turnAngle;

            target.localRotation = Quaternion.Euler(target.eulerAngles.x, 0, target.eulerAngles.z);
        }

        if (userSettings.rightMouseButtonCombo.IsResolved)
        {
            DragY(character, userSettings.invertXAxis);
            DragX(target, userSettings.invertYAxis);
        }
    }

    private void DragY(Transform target, bool invert)
    {
        float mouseX = Input.GetAxis("Mouse X") * userSettings.sensitivities.x * Time.deltaTime;
        mouseX = invert ? -mouseX : mouseX;

        target.Rotate(Vector3.up * mouseX);
    }

    private void DragX(Transform target, bool invert)
    {
        float mouseY = Input.GetAxis("Mouse Y") * userSettings.sensitivities.y * Time.deltaTime;
        mouseY = invert ? -mouseY : mouseY;

        var ea = target.rotation.eulerAngles;

        ea.x += mouseY;

        ea.x = ClampAngle(ea.x, systemSettings.clamps.x, systemSettings.clamps.y);
        target.eulerAngles = ea;
    }

    private void Drag(Transform target, Vector2 sensitivities, string xAxis, string yAxis, bool invertXAxis, bool invertYAxis, CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod xRotationMethod, Vector2 clamps)
    {
        var x = Input.GetAxis(xAxis) * sensitivities.x * Time.deltaTime; ;
        var y = Input.GetAxis(yAxis) * sensitivities.y * Time.deltaTime;

        var yDelta = invertYAxis ? -y : y;
        var xDelta = invertXAxis ? -x : x;

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

    public void LerpToDefaultEulerAngles(Transform target)
    {
        var targetEulers = target.eulerAngles;

        if (target.rotation.y != Quaternion.Euler(userSettings.defaultEulerAngles).y)
        {
            targetEulers.y = userSettings.defaultEulerAngles.y;
        }

        if (target.rotation.z != Quaternion.Euler(userSettings.defaultEulerAngles).z)
        {
            targetEulers.z = userSettings.defaultEulerAngles.z;
        }

        if (target.rotation.x != Quaternion.Euler(userSettings.defaultEulerAngles).x)
        {
            targetEulers.x = userSettings.defaultEulerAngles.x;
        }

        var targetRot = character.rotation * Quaternion.Euler(targetEulers);

        LerpToEulerAngles(target, targetRot, userSettings.defaultSpeed);
    }


    private void LerpToEulerAngles(Transform transform, Quaternion targetRot, float speed)
    {
        var result = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * speed);
        transform.rotation = result;
    }
}
