using UnityEngine;

[CreateAssetMenu(fileName = "Bob - User Settings", menuName = "Scriptable Objects/Camera/Components/Bob - User Settings")]
public class BobUserSettingsScriptableObject : ScriptableObject
{
    public Vector3 periods = new(0, 0.5f, 0);
    public Vector3 amplitudes = new(0, 0.2f, 0);
}
