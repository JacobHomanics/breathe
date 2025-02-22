using UnityEngine;

public class Collision : CameraControllerComponent
{
    public Transform cam;
    public Transform target;

    public CollisionSystemSettingsScriptableObject systemSettings;

    public override Vector3 Calculate(Vector3 point)
    {
        return HandleObstacleCollision(target.position, point); //cam.InverseTransformPoint(HandleObstacleCollision(cam.TransformPoint(point), target.position));
    }

    private Vector3 HandleObstacleCollision(Vector3 start, Vector3 end)
    {
        // Debug.DrawLine(start, end, Color.red);

        // Debug.DrawRay(start, end, Color.gray);

        if (Physics.Linecast(start, end, out RaycastHit hit, systemSettings.layerMask))
        {
            // Debug.DrawLine(end, hit.point, Color.green);

            return new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        return end;
    }
}
