using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRotatorScriptableObject", menuName = "Scriptable Objects/PlayerRotatorScriptableObject")]
public class PlayerRotatorScriptableObject : ScriptableObject
{
    // WoW style config:
    // left: 150f
    // right: 150f

    public float leftTurnSpeed = 150f;
    public float rightTurnSpeed = 150f;
}
