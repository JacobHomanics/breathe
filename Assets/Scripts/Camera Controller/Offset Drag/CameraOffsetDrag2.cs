using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOffsetDrag2 : MonoBehaviour
{
    public Transform cam;

    public Transform target;
    public Transform character;
    public PlayerMotor motor;
    public PlayerRotator rotator;

    public CameraOffsetDragUserSettingsScriptableObject userSettings;
    public CameraOffsetDragSystemSettingsScriptableObject systemSettings;

    public CameraOffsetDragControls controls;
    public bool isFirstFrameRightDragEnabled { get; private set; }

    private Quaternion initialRelativeRotation;

    void Start()
    {
        // target.rotation = Quaternion.Euler(userSettings.defaultEulerAngles);
        currentDistance = defaultDistance;
        desiredDistance = defaultDistance;
        initialRelativeRotation = Quaternion.Inverse(target.rotation) * cam.transform.rotation;
    }

    void LateUpdate()
    {
        Calculate();
    }

    private void Calculate()
    {
        var result = controls.Calculate();

        isFirstFrameRightDragEnabled = result.Item3 && !isFirstFrameRightDragEnabled;
        bool firstFrame = isFirstFrameRightDragEnabled;
        isFirstFrameRightDragEnabled = false;

        Drag(result.Item1, result.Item2, result.Item3, firstFrame);
    }

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;

    private float desiredDistance;
    private float correctedDistance;

    public float zoomSensitivity;
    public float thresholdPercentage = 1f; // For example, 10%

    public float minDistance;
    public float maxDistance;
    public float targetHeight;
    public LayerMask collisionLayers;
    public float offsetFromWall;
    public float defaultDistance;
    public float zoomDampening;

    private float currentDistance;
    public float rotationDampening;

    private void Drag(bool isDragEnabled, bool isLeftDragEnabled, bool isRightDragEnabled, bool firstFrame)
    {

        if (isDragEnabled)
        {
            yDeg -= Input.GetAxis("Mouse Y") * userSettings.Sensitivities.y * 0.02f;
            xDeg += Input.GetAxis("Mouse X") * userSettings.Sensitivities.x * 0.02f;
        }

        yDeg = ClampAngle2(yDeg, systemSettings.clamps.x, systemSettings.clamps.y);

        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);

        Quaternion targetRotation = target.rotation * rotation;


        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSensitivity;
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        var vTargetOffset = new Vector3(0, -targetHeight, 0);

        var position = target.position - (targetRotation * Vector3.forward * desiredDistance + vTargetOffset);

        var trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

        RaycastHit collisionHit;

        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers))
        {
            // Calculate the distance from the original estimated position to the collision location,
            // subtracting out a safety "offset" distance from the object we hit.  The offset will help
            // keep the camera from being right on top of the surface we hit, which usually shows up as
            // the surface geometry getting partially clipped by the camera's front clipping plane.
            correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetFromWall;
        }

        currentDistance = Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening);

        position = target.position - (targetRotation * Vector3.forward * currentDistance + vTargetOffset);




        cam.transform.rotation = targetRotation;
        cam.transform.position = position;

        if (isRightDragEnabled)
        {
            target.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        }


        // if (!isDragEnabled && (motor.IsMoving || rotator.IsRotating))
        // {
        //     xDeg = Mathf.LerpAngle(xDeg, 0, rotationDampening * Time.deltaTime);
        // }


        // var targetRotationAngle = target.eulerAngles.y;
        // var currentRotationAngle = transform.eulerAngles.y;

        // xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);

        // float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentRotationAngle, targetRotationAngle));
        // float maxAllowedDifference = thresholdPercentage / 100f * 360f;

        // if (angleDifference <= maxAllowedDifference)
        // {
        //     xDeg = target.eulerAngles.y;
        // }
        // else
        // {
        //     xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
        // }






        // if (angleDifference <= maxAllowedDifference)
        // {
        //     xDeg = targetRotationAngle;
        // }
        // else
        // {
        //     if ((motor.IsMoving || rotator.IsRotating) && !isDragEnabled)
        //     {
        //         if (angleDifference > maxAllowedDifference)
        //             xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);

        //     }
        // }


        // if ((motor.IsMoving || rotator.IsRotating) && !isDragEnabled)
        // {
        //     float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentRotationAngle, targetRotationAngle));

        //     // Calculate the maximum difference based on the threshold
        //     float maxAllowedDifference = thresholdPercentage / 100f * 360f;
        //     xDeg = targetRotationAngle;

        //     if (angleDifference > maxAllowedDifference)
        //         xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);

        // }







        // if (!isDragEnabled)
        // {
        //     if (motor.IsForwardActivated || motor.IsBackwardActivated)
        //         LerpToDefaultEulerAngles(target);

        //     return;
        // }

        // if (firstFrame)
        // {
        //     Quaternion turnAngle = Quaternion.Euler(0, target.eulerAngles.y, 0);
        //     character.rotation = turnAngle;

        //     target.localRotation = Quaternion.Euler(target.eulerAngles.x, 0, target.eulerAngles.z);
        // }

        // if (isRightDragEnabled)
        // {
        //     DragY(character, userSettings.invertXAxis);
        //     DragX(target, userSettings.invertYAxis);
        // }
        // else if (isLeftDragEnabled)
        // {
        //     Drag(target, userSettings.Sensitivities, userSettings.xAxis, userSettings.yAxis, userSettings.invertXAxis, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);
        // }
    }



    private void Drag(Transform target, Vector2 sensitivities, string xAxis, string yAxis, bool invertXAxis, bool invertYAxis, CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod xRotationMethod, Vector2 clamps)
    {
        var x = Input.GetAxis(xAxis) * sensitivities.x * Time.deltaTime;
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


    private void DragX(Transform target, bool invert)
    {
        float mouseY = Input.GetAxis("Mouse Y") * userSettings.Sensitivities.y * Time.deltaTime;
        mouseY = invert ? -mouseY : mouseY;

        var ea = target.rotation.eulerAngles;

        ea.x += mouseY;

        ea.x = ClampAngle(ea.x, systemSettings.clamps.x, systemSettings.clamps.y);
        target.eulerAngles = ea;
    }

    private void DragY(Transform target, bool invert)
    {
        float mouseX = Input.GetAxis("Mouse X") * userSettings.Sensitivities.x * Time.deltaTime;
        mouseX = invert ? -mouseX : mouseX;

        target.Rotate(Vector3.up * mouseX);
    }

    float ClampAngle2(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
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

        if (target.rotation.x != Quaternion.Euler(userSettings.defaultEulerAngles).x)
        {
            targetEulers.x = userSettings.defaultEulerAngles.x;
        }

        if (target.rotation.y != Quaternion.Euler(userSettings.defaultEulerAngles).y)
        {
            targetEulers.y = userSettings.defaultEulerAngles.y;
        }

        if (target.rotation.z != Quaternion.Euler(userSettings.defaultEulerAngles).z)
        {
            targetEulers.z = userSettings.defaultEulerAngles.z;
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
