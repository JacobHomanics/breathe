using UnityEngine;

public class CameraOffsetDrag : MonoBehaviour
{
    public Transform cam;
    public Transform target;
    public Transform character;
    public PlayerMotor motor;

    public CameraOffsetDragUserSettingsScriptableObject userSettings;
    public CameraOffsetDragSystemSettingsScriptableObject systemSettings;

    public float cursorHideThresholdOnDrag = 100f;
    public Vector3 mouseStartPosOnDrag;
    public Vector3 previousMousePositionDuringDrag;


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
        var isDragEnabled = userSettings.leftMouseButtonCombo.IsResolved;

        // if (motor.IsForwardActivated || motor.IsBackwardActivated)
        // {
        //     if (!isDragEnabled)
        //         LerpToDefaultEulerAngles(target);
        // }

        // if (isDragEnabled)
        //     mouseStartPosOnDrag = Input.mousePosition;
        // // if ()

        // if (IsDragThresholdReached)
        // {
        //     // isRightClickDragEnabled = true;
        //     // Cursor.visible = false;
        //     // isDragEnabled = true;
        // }
        // else
        // {
        //     // Cursor.visible = true;
        // }

        // previousMousePositionDuringDrag = Input.mousePosition;

        // UpdateCenterPointPosition(target);


        // if (isDragEnabled || userSettings.rightMouseButtonCombo.IsResolved)
        //     Drag(target, userSettings.sensitivities, userSettings.xAxis, userSettings.yAxis, userSettings.invertXAxis, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);

        // if (userSettings.rightMouseButtonCombo.IsResolved)
        // {
        //     Quaternion turnAngle = Quaternion.Euler(0, target.eulerAngles.y, 0);
        //     character.rotation = turnAngle;
        // }

    }

    // private void UpdateCenterPointPosition(Transform target)
    // {
    //     //Resets to character position as a base
    //     target.position = character.position;

    //     //Sets the x and z values to the offset amount based on the direction of the center point
    //     target.position += target.TransformDirection(0, 0, 0); //centerPointOffsetPosition.x, 0, centerPointOffsetPosition.z);
    //                                                            //Sets the y position to a flat offset position with no direction accounted for
    //                                                            //this is done so that the y stays the same regardless of the center point direction
    //     target.position += new Vector3(0, 0, 0); //new Vector3(0, centerPointOffsetPosition.y, 0);

    // }


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
