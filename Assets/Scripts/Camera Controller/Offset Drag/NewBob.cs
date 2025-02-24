using UnityEngine;

public class NewBob : CameraControllerComponent
{
    public BobUserSettingsScriptableObject userSettings;
    public Transform pivot;

    void Update()
    {
        pivot.position = Calculate(pivot.position);
    }

    public override Vector3 Calculate(Vector3 position)
    {
        return HandleBob(position, userSettings.periods, userSettings.amplitudes);
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

}
