using UnityEngine;

[CreateAssetMenu(fileName = "Offset Drag - User Settings", menuName = "Scriptable Objects/Camera/Offset Drag/User Settings")]
public class CameraOffsetDragUserSettingsScriptableObject : ScriptableObject
{
    public bool invertXAxis;
    public bool invertYAxis = true;

    public string xAxis = "Mouse X";
    public string yAxis = "Mouse Y";


    public Vector2 webGlSensitivities = new(30, 30);

    public Vector2 nonWebGlSensitivities = new(1000, 1000);

    public Vector2 Sensitivities
    {
        get
        {
#if UNITY_EDITOR
            return nonWebGlSensitivities;
#elif UNITY_WEBGL
            return webGlSensitivities;
#else
            return nonWebGlSensitivities;
#endif
        }
        set
        {
#if UNITY_EDITOR
            nonWebGlSensitivities = value;
#elif UNITY_WEBGL
            webGlSensitivities = value;
#else
            nonWebGlSensitivities = value;
#endif      
        }
    }

    public Vector3 defaultEulerAngles = new(35f, 0f, 0f);
    public float defaultSpeed = 1f;

    public float cursorHideThresholdOnDrag = 100f;

}
