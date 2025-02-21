using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOffsetDragControls : MonoBehaviour
{
    public CameraOffsetDragSystemSettingsScriptableObject systemSettings;


    // public bool isLeftDragDown;
    // public bool isRightDragDown;
    // public bool isAnyDragDown;

    // public bool isLeftDragInitiated;
    // public bool isRightDragInitiated;

    // public bool isAnyDragInitiated;

    // public bool isAllDragInitiated;

    // public bool isLeftDragEnabled;
    // public bool isRightDragEnabled;
    // public bool isFirstFrameRightDragEnabled;
    // public bool isDragToBeSetToEnabled;

    public bool isDragEnabled;

    public bool isCursorThresholdReached;

    public Vector3 totalDistance;
    public Vector3 MousePosOnDragStart;

    public (bool, bool, bool) Calculate()
    {
        bool isLeftDragDown = Input.GetMouseButtonDown(0);
        bool isRightDragDown = Input.GetMouseButtonDown(1);
        bool isAnyDragDown = isLeftDragDown || isRightDragDown;

        bool isLeftDragInitiated = Input.GetMouseButton(0);
        bool isRightDragInitiated = Input.GetMouseButton(1);
        bool isAnyDragInitiated = isLeftDragInitiated || isRightDragInitiated;

        bool isAllDragInitiated = isLeftDragInitiated && isRightDragInitiated;

        if ((isLeftDragDown && !isRightDragInitiated) ||
            (isRightDragDown && !isLeftDragInitiated))
        {
            totalDistance = default;
            MousePosOnDragStart = Input.mousePosition;

        }

        if (isAnyDragInitiated)
        {
            totalDistance += new Vector3(Mathf.Abs(Input.mousePositionDelta.x), Mathf.Abs(Input.mousePositionDelta.y), 0);
        }

        isCursorThresholdReached = isAnyDragInitiated && (totalDistance.x >= systemSettings.cursorHideThresholdOnDrag || totalDistance.y >= systemSettings.cursorHideThresholdOnDrag);

        bool isDragToBeSetToEnabled = isAllDragInitiated || isCursorThresholdReached;


        if (isDragToBeSetToEnabled)
        {
            isDragEnabled = true;
        }

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            isDragEnabled = false;
        }

        if ((Input.GetMouseButtonUp(0) && !isDragEnabled) || (Input.GetMouseButtonUp(1) && !isDragEnabled))
        {
            Mouse.current.WarpCursorPosition(MousePosOnDragStart);
        }

        Cursor.visible = !isDragEnabled;

        bool isLeftDragEnabled = isDragEnabled && isLeftDragInitiated;
        bool isRightDragEnabled = isDragEnabled && isRightDragInitiated;
        return (isDragEnabled, isLeftDragEnabled, isRightDragEnabled);
    }


}
