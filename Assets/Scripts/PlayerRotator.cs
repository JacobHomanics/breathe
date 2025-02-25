using UnityEngine;
using UnityEngine.Events;

public class PlayerRotator : MonoBehaviour
{
    public PlayerRotatorScriptableObject motorValues;
    public PlayerRotatorKeybindsScriptableObject keybinds;

    public bool forceBidirectionalInputBlocking = false;

    public Events events = new();

    public bool IsRotating
    {
        get
        {
            return RightMotionTrueCount > 0 || LeftMotionTrueCount > 0;
        }
    }
    public int RightMotionTrueCount { get { return Combo.GetResolveCount(keybinds.rightTurnCombos); } }

    public int LeftMotionTrueCount { get { return Combo.GetResolveCount(keybinds.leftTurnCombos); } }

    private void HandleRotation()
    {
        if (forceBidirectionalInputBlocking)
        {
            if (LeftMotionTrueCount > 0 && RightMotionTrueCount > 0)
                return;
        }

        if (LeftMotionTrueCount > 0)
        {
            transform.Rotate(0, -motorValues.leftTurnSpeed * Time.deltaTime, 0);
            events.TurnedLeft?.Invoke();
            events.Turned?.Invoke();
        }

        if (RightMotionTrueCount > 0)
        {
            transform.Rotate(0, motorValues.rightTurnSpeed * Time.deltaTime, 0);
            events.TurnedRight?.Invoke();
            events.Turned?.Invoke();
        }
    }

    private void Update()
    {
        HandleRotation();
    }

    [System.Serializable]
    public class Events
    {
        public UnityEvent Turned = new();
        public UnityEvent TurnedLeft = new();
        public UnityEvent TurnedRight = new();
    }
}