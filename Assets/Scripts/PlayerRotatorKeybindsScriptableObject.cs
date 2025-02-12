using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRotatorKeybindsScriptableObject", menuName = "Scriptable Objects/PlayerRotatorKeybindsScriptableObject")]
public class PlayerRotatorKeybindsScriptableObject : ScriptableObject
{
    // WoW style config:
    // left: A / Left Arrow
    // right: D / Right Arrow

    public Combo[] leftTurnCombos;
    public Combo[] rightTurnCombos;
}
