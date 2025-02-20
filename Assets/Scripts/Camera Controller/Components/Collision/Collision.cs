using UnityEngine;

public class Collision : CameraControllerComponent
{
    public Transform cam;
    public Transform target;

    public CollisionSystemSettingsScriptableObject systemSettings;

    public override Vector3 Calculate(Vector3 point)
    {
        return cam.InverseTransformPoint(HandleObstacleCollision(cam.TransformPoint(point), target.position));
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
