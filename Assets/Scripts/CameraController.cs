using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cam;
    public Transform target;

    public CameraControllerSystemSettingsScriptableObject systemSettings;
    public CameraControllerUserSettingsScriptableObject userSettings;

    public float Zoom { get; private set; }

    void Start()
    {
        Zoom = userSettings.zoom;
    }

    void LateUpdate()
    {
        Calculate(target);
    }

    private void Calculate(Transform target)
    {
        Vector3 targetPosition = Vector3.zero;
        targetPosition = HandleZoom(targetPosition);
        targetPosition = HandleBob(targetPosition, userSettings.periods, userSettings.amplitudes);
        targetPosition = cam.InverseTransformPoint(HandleObstacleCollision(cam.TransformPoint(targetPosition), target.position));

        if (Input.GetMouseButton(0))
            Drag(target, userSettings.sensitivities, userSettings.invertYAxis, systemSettings.xRotationMethod, systemSettings.clamps);

        cam.localPosition = Vector3.Lerp(cam.localPosition, targetPosition, Time.deltaTime * userSettings.smoothing);
    }

    private void Drag(Transform target, Vector2 sensitivities, bool invertYAxis, CameraControllerSystemSettingsScriptableObject.XRotationMethod xRotationMethod, Vector2 clamps)
    {
        var yFrame = GetY(invertYAxis) * sensitivities.y * Time.deltaTime;
        var xFrame = Input.GetAxis("Mouse X") * sensitivities.x * Time.deltaTime;

        target.rotation = Quaternion.AngleAxis(xFrame, Vector3.up) * target.rotation;

        if (xRotationMethod == CameraControllerSystemSettingsScriptableObject.XRotationMethod.AngleAxis)
            target.rotation = Quaternion.AngleAxis(yFrame, target.right) * target.rotation;

        var ea = target.rotation.eulerAngles;

        if (xRotationMethod == CameraControllerSystemSettingsScriptableObject.XRotationMethod.EulerAngles)
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

    private Vector3 HandleBob(Vector3 position, Vector3 periods, Vector3 amplitudes)
    {
        return new Vector3(
            position.x + GetDistanceFromTheta(periods.x, amplitudes.x),
            position.y + GetDistanceFromTheta(periods.y, amplitudes.y),
            position.z + GetDistanceFromTheta(periods.z, amplitudes.z)
        );
    }

    private float GetDistanceFromTheta(float period, float amplitude)
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);

        if (float.IsNaN(distance))
            distance = 0;

        return distance;
    }

    private Vector3 HandleZoom(Vector3 position)
    {
        return position += new Vector3(0, 0, -HandleZoom2());
    }

    private float HandleZoom2()
    {
        if (userSettings.invertZoomAxis)
            Zoom -= Input.GetAxis(userSettings.zoomAxis) * userSettings.zoomSensitivity * Time.deltaTime;
        else
            Zoom += Input.GetAxis(userSettings.zoomAxis) * userSettings.zoomSensitivity * Time.deltaTime;

        if (Zoom > userSettings.maxZoom)
            Zoom = userSettings.maxZoom;

        if (Zoom < systemSettings.minZoom)
            Zoom = systemSettings.minZoom;

        return Zoom;
    }

    private Vector3 HandleObstacleCollision(Vector3 start, Vector3 end)
    {
        if (Physics.Linecast(start, end, out RaycastHit hit, systemSettings.layerMask))
        {
            return new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        return start;
    }
}
