using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMotorScriptableObject", menuName = "Scriptable Objects/PlayerMotorScriptableObject")]
public class PlayerMotorScriptableObject : ScriptableObject
{
    // WoW style config:
    // left: 2.5f
    // right: 2.5f
    // forward: 2.5f
    // backward: 1.65f
    // gravity: (0, 9.81f, 0)
    // jumpPower: 15
    // midAirMovementPercentage: 0.55f

    public float leftMoveSpeed = 2.5f;
    public float rightMoveSpeed = 2.5f;
    public float forwardMoveSpeed = 2.5f;
    public float backwardMoveSpeed = 1.65f;
    public Vector3 gravity = new(0, 9.81f, 0);
    public float jumpPower = 15f;
    public float midAirMovementPercentage = .55f;
}
