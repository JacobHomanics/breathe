using UnityEngine;

public abstract class CameraControllerComponent : MonoBehaviour
{
    public abstract Vector3 Calculate(Vector3 point);
}
