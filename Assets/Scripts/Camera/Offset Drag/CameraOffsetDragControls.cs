using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOffsetDragControls : MonoBehaviour
{
    public CameraOffsetDragUserSettingsScriptableObject userSettings;

    public bool IsDragEnabled { get; private set; }

    public bool IsCursorThresholdReached { get; private set; }

    public Vector3 TotalDistance { get; private set; }
    public Vector3 MousePosOnDragStart { get; private set; }

    public (bool, bool, bool) Calculate()
    {
        bool isLeftDragDown = Input.GetMouseButtonDown(0);
        bool isRightDragDown = Input.GetMouseButtonDown(1);
        // bool isAnyDragDown = isLeftDragDown || isRightDragDown;

        bool isLeftDragInitiated = Input.GetMouseButton(0);
        bool isRightDragInitiated = Input.GetMouseButton(1);
        bool isAnyDragInitiated = isLeftDragInitiated || isRightDragInitiated;

        bool isAllDragInitiated = isLeftDragInitiated && isRightDragInitiated;

        if ((isLeftDragDown && !isRightDragInitiated) ||
            (isRightDragDown && !isLeftDragInitiated))
        {
            TotalDistance = default;

        }

        if (isAnyDragInitiated)
        {
            TotalDistance += new Vector3(Mathf.Abs(Input.mousePositionDelta.x), Mathf.Abs(Input.mousePositionDelta.y), 0);
        }

        IsCursorThresholdReached = isAnyDragInitiated && (TotalDistance.x >= userSettings.cursorHideThresholdOnDrag || TotalDistance.y >= userSettings.cursorHideThresholdOnDrag);

        bool isDragToBeSetToEnabled = isAllDragInitiated || IsCursorThresholdReached;


        if (isDragToBeSetToEnabled)
        {
            if (!IsDragEnabled)
                MousePosOnDragStart = Input.mousePosition;

            IsDragEnabled = true;
        }

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            IsDragEnabled = false;
        }

        if (IsDragEnabled)
            Mouse.current.WarpCursorPosition(MousePosOnDragStart);

        Cursor.visible = !IsDragEnabled;

        bool isLeftDragEnabled = IsDragEnabled && isLeftDragInitiated;
        bool isRightDragEnabled = IsDragEnabled && isRightDragInitiated;
        return (IsDragEnabled, isLeftDragEnabled, isRightDragEnabled);
    }


}
