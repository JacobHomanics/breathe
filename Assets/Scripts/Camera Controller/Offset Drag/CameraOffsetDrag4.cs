using Unity.Cinemachine;
using UnityEngine;

public class CameraOffsetDrag4 : MonoBehaviour
{
    public Transform target;
    public Transform character;
    public PlayerMotor motor;
    public PlayerRotator rotator;
    public CameraOffsetDragUserSettingsScriptableObject userSettings;
    public CameraOffsetDragSystemSettingsScriptableObject systemSettings;

    public CameraOffsetDragControls controls;
    public bool isFirstFrameRightDragEnabled { get; private set; }

    public Transform cnCamTarget;
    public Transform cam;
    // void Start()
    // {
    //     _cinemachineTargetYaw = cnCamTarget.rotation.eulerAngles.y;
    // }

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

    public float CameraAngleOverride = 0.0f;
    public float minPitch = -80;
    public float maxPitch = 80;

    public float CameraSpeed = 10;

    public Transform tilt;
    public Transform pivot;


    // // cinemachine
    // private float _cinemachineTargetYaw;
    // private float _cinemachineTargetPitch;

    float currentPan, currentTilt = 10;

    private void Drag(bool isDragEnabled, bool isLeftDragEnabled, bool isRightDragEnabled, bool firstFrame)
    {
        if (!isDragEnabled)
        {
            if (motor.IsMoving || rotator.IsRotating)
            {

            }
            // if (motor.IsForwardActivated || motor.IsBackwardActivated)
            //     LerpToDefaultEulerAngles(target);

            // return;
        }

        if (firstFrame)
        {
            Quaternion turnAngle = Quaternion.Euler(0, target.eulerAngles.y, 0);
            character.rotation = turnAngle;

            pivot.localRotation = Quaternion.Euler(pivot.eulerAngles.x, 0, pivot.eulerAngles.z);
        }


        if (isRightDragEnabled)
        {
            DragY(character, userSettings.invertXAxis);
            // Quaternion turnAngle = Quaternion.Euler(0, pivot.eulerAngles.y, 0);
            // character.rotation = turnAngle;

            // float _targetRotation = cam.eulerAngles.y; //Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
            //                                            //_mainCamera.transform.eulerAngles.y;
            //                                            // float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
            //                                            //     RotationSmoothTime);

            // // rotate to face input direction relative to camera position
            // character.transform.rotation = Quaternion.Euler(0.0f, _targetRotation, 0.0f);

            // var inputX = userSettings.Sensitivities.x * Input.GetAxis("Mouse X") * Time.deltaTime;
            // var inputY = userSettings.Sensitivities.y * Input.GetAxis("Mouse Y") * Time.deltaTime;
            // _cinemachineTargetYaw += userSettings.invertXAxis ? -inputX : inputX;
            // _cinemachineTargetPitch += userSettings.invertYAxis ? -inputY : inputY;


            // // clamp our rotations so our values are limited 360 degrees
            // _cinemachineTargetYaw = ClampAngle2(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            // _cinemachineTargetPitch = ClampAngle2(_cinemachineTargetPitch, minPitch, maxPitch);

            // // Cinemachine will follow this target
            // cnCamTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            //     _cinemachineTargetYaw, 0.0f);

            // character.rotation = Quaternion.Euler(0, target.eulerAngles.y, 0);
            // DragY(character, userSettings.invertXAxis);
            // DragX(target, userSettings.invertYAxis);
        }

        else if (isLeftDragEnabled)
        {
            var x = Input.GetAxis("Mouse X") * userSettings.Sensitivities.x * Time.deltaTime;
            var y = Input.GetAxis("Mouse Y") * userSettings.Sensitivities.y * Time.deltaTime;

            var yDelta = userSettings.invertYAxis ? -y : y;
            var xDelta = userSettings.invertXAxis ? -x : x;

            pivot.rotation *= Quaternion.AngleAxis(xDelta, Vector3.up);// * pivot.rotation;
            // tilt.rotation *= Quaternion.AngleAxis(yDelta, Vector3.right);// * tilt.rotation;
            var eulerAngles = tilt.eulerAngles;
            eulerAngles.x += yDelta;
            eulerAngles.x = ClampAngle(eulerAngles.x, minPitch, maxPitch);
            tilt.eulerAngles = eulerAngles;


            // var inputX = userSettings.Sensitivities.x * Input.GetAxis("Mouse X") * Time.deltaTime;
            // var inputY = userSettings.Sensitivities.y * Input.GetAxis("Mouse Y") * Time.deltaTime;

            // currentTilt += userSettings.invertYAxis ? -inputY : inputY;
            // currentTilt = Mathf.Clamp(currentTilt, minPitch, maxPitch);
            // currentPan += userSettings.invertXAxis ? -inputX : inputX; ;


            // pivot.transform.eulerAngles = new Vector3(pivot.transform.eulerAngles.x, currentPan, pivot.transform.eulerAngles.z);
            // tilt.eulerAngles = new Vector3(currentTilt, tilt.eulerAngles.y, tilt.eulerAngles.z);

            // _cinemachineTargetYaw += userSettings.invertXAxis ? -inputX : inputX;
            // _cinemachineTargetPitch += userSettings.invertYAxis ? -inputY : inputY;


            // // clamp our rotations so our values are limited 360 degrees
            // _cinemachineTargetYaw = ClampAngle2(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            // _cinemachineTargetPitch = ClampAngle2(_cinemachineTargetPitch, minPitch, maxPitch);

            // // Cinemachine will follow this target
            // cnCamTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            //     _cinemachineTargetYaw, 0.0f);

            // currentTilt -= Input.GetAxis("Mouse Y") * CameraSpeed;
            // currentTilt = Mathf.Clamp(currentTilt, minPitch, maxPitch);

            // tilt.eulerAngles = new Vector3(currentTilt, tilt.eulerAngles.y, tilt.eulerAngles.z);
        }

        // pivot.transform.position = character.transform.position;





        // pivot.position = character.position;
        // pivot.rotation = character.rotation;
        // Drag3(target, userSettings.Sensitivities, userSettings.xAxis, userSettings.yAxis, userSettings.invertXAxis, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);


        // target.transform.position = character.transform.position;
        // target.transform.rotation = character.transform.rotation;


        // Drag2(target, userSettings.Sensitivities, userSettings.xAxis, userSettings.yAxis, userSettings.invertXAxis, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);


        // // target.transform.rotation = character.transform.rotation;

        // // target.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
        // target.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 1, Vector3.up);


        // Quaternion newRotation = target.transform.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 1, Vector3.right);

        // // target.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 1, Vector3.right);

        // Vector3 eulerAngles = newRotation.eulerAngles;

        // eulerAngles.x = ClampAngle(eulerAngles.y, minPitch, maxPitch);

        // // Clamp the X rotation (pitch) between min and max values
        // // eulerAngles.x = Mathf.Clamp(eulerAngles.x, minPitch, maxPitch);

        // // Convert the Euler angles back to a Quaternion and apply it
        // target.transform.rotation = Quaternion.Euler(eulerAngles);

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


    private void Drag3(Transform target, Vector2 sensitivities, string xAxis, string yAxis, bool invertXAxis, bool invertYAxis, CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod xRotationMethod, Vector2 clamps)
    {
        var x = Input.GetAxis(xAxis) * sensitivities.x * Time.deltaTime;
        var y = Input.GetAxis(yAxis) * sensitivities.y * Time.deltaTime;

        var yDelta = invertYAxis ? -y : y;
        var xDelta = invertXAxis ? -x : x;

        target.rotation = Quaternion.AngleAxis(xDelta, Vector3.up) * target.rotation;

        var ea = target.rotation.eulerAngles;
        ea.x += yDelta;
        ea.x = ClampAngle(ea.x, clamps.x, clamps.y);
        target.eulerAngles = ea;
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


    private float yaw;
    private float pitch;

    private void Drag2(Transform target, Vector2 sensitivities, string xAxis, string yAxis, bool invertXAxis, bool invertYAxis, CameraOffsetDragSystemSettingsScriptableObject.XRotationMethod xRotationMethod, Vector2 clamps)
    {
        var x = Input.GetAxis(xAxis) * sensitivities.x * Time.deltaTime;
        var y = Input.GetAxis(yAxis) * sensitivities.y * Time.deltaTime;

        var yDelta = invertYAxis ? -y : y;
        var xDelta = invertXAxis ? -x : x;

        yaw += yDelta;
        pitch += xDelta;

        var eulerAngles = target.eulerAngles;
        eulerAngles.x = yaw;
        eulerAngles.x = ClampAngle2(eulerAngles.x, clamps.x, clamps.y);
        eulerAngles.y = pitch;

        target.rotation = Quaternion.Euler(eulerAngles);








        // target.rotation = Quaternion.AngleAxis(xDelta, Vector3.up) * target.rotation;

        // var ea = target.rotation.eulerAngles;

        // ea.x += yDelta;

        // ea.x = ClampAngle(ea.x, clamps.x, clamps.y);
        // target.eulerAngles = ea;
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

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    float ClampAngle2(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
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
