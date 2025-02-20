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
        cam.localPosition = Vector3.Lerp(cam.localPosition, targetPosition, Time.deltaTime * userSettings.smoothing);
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
