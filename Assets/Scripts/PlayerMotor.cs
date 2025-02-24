using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMotor : MonoBehaviour
{
    [Header("References")]
    public CharacterController characterController;

    public PlayerMotorScriptableObject motorValues;
    public PlayerMotorKeybindsScriptableObject keybinds;

    public Events events = new();

    public Vector3 FinalMovement { get; private set; }
    public Vector3 NormalizedInputMoveDirection { get; private set; }

    public bool IsGroundedCheckedOnGround { get; private set; }

    private Vector3 jumpVelocity;

    public bool IsMoving
    {
        get
        {
            return IsForwardActivated || IsBackwardActivated || IsLeftActivated || IsRightActivated;
        }
    }
    public bool IsForwardActivated { get { return ForwardMotionTrueCount > BackwardMotionTrueCount; } }
    public bool IsBackwardActivated { get { return BackwardMotionTrueCount > ForwardMotionTrueCount; } }
    public bool IsLeftActivated { get { return LeftMotionTrueCount > RightMotionTrueCount; } }
    public bool IsRightActivated { get { return RightMotionTrueCount > LeftMotionTrueCount; } }

    public int ForwardMotionTrueCount { get { return Combo.GetResolveCount(keybinds.fowardCombos); } }
    public int BackwardMotionTrueCount { get { return Combo.GetResolveCount(keybinds.backwardCombos); } }
    public int RightMotionTrueCount { get { return Combo.GetResolveCount(keybinds.rightCombos); } }
    public int LeftMotionTrueCount { get { return Combo.GetResolveCount(keybinds.leftCombos); } }
    public int JumpMotionTrueCount { get { return Combo.GetResolveCount(keybinds.jumpCombos); } }

    public bool AreAllKeyCodeInputsTrue(KeyCode[] keyCodes)
    {
        return keyCodes.All(keyCode => Input.GetKey(keyCode));
    }

    public bool AreAllMouseButtonInputsTrue(int[] mouseButtons)
    {
        return mouseButtons.All(button => Input.GetMouseButton(button));
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        FinalMovement = default;

        if (characterController.isGrounded)
            HandleInput();

        HandleGravity();

        HandleJumpAndMidairMovement();

        characterController.Move(FinalMovement * Time.deltaTime);
    }

    private void Jump()
    {
        jumpVelocity.y = motorValues.jumpPower;

        if (IsLeftActivated)
            jumpVelocity.x = -motorValues.leftMoveSpeed;

        if (IsRightActivated)
            jumpVelocity.x = motorValues.rightMoveSpeed;

        if (IsForwardActivated)
            jumpVelocity.z = motorValues.forwardMoveSpeed;

        if (IsBackwardActivated)
            jumpVelocity.z = -motorValues.backwardMoveSpeed;

        events.otherEvents.Jumped?.Invoke();
    }

    private void JumpIfGrounded()
    {
        if (characterController.isGrounded)
            Jump();
    }

    private void CheckInputToJumpIfGrounded()
    {
        if (JumpMotionTrueCount > 0)
            JumpIfGrounded();
    }

    private void CheckForGround()
    {
        if (characterController.isGrounded)
        {
            //is grounded and invoking grounded functions
            if (!IsGroundedCheckedOnGround)
            {
                //currentJumpPower = 0f;

                jumpVelocity = default;
                IsGroundedCheckedOnGround = true;
                events.otherEvents.Grounded?.Invoke();
            }
            //is grounded but already invoked grounded functions
            else { }
        }
        //is not grounded
        else
        {
            IsGroundedCheckedOnGround = false;
            CheckForMidAirMovement();
        }
    }

    private void CheckForMidAirMovement()
    {
        if (jumpVelocity.x == 0 && jumpVelocity.z == 0)
        {
            if (IsLeftActivated)
                jumpVelocity.x = -motorValues.leftMoveSpeed * motorValues.midAirMovementPercentage;

            if (IsRightActivated)
                jumpVelocity.x = motorValues.rightMoveSpeed * motorValues.midAirMovementPercentage;

            if (jumpVelocity.x == 0)
            {
                if (IsForwardActivated)
                    jumpVelocity.z = motorValues.forwardMoveSpeed * motorValues.midAirMovementPercentage;
                if (IsBackwardActivated)
                    jumpVelocity.z = -motorValues.backwardMoveSpeed * motorValues.midAirMovementPercentage;
            }
        }
    }

    private Vector3 HandleJumpAndMidairMovement()
    {
        CheckInputToJumpIfGrounded();

        jumpVelocity.y -= Time.deltaTime * motorValues.jumpPower;
        if (jumpVelocity.y < 0f)
            jumpVelocity.y = 0f;

        CheckForGround();

        Move(transform.TransformDirection(jumpVelocity));

        return transform.TransformDirection(jumpVelocity);
    }


    private void HandleInput()
    {
        Vector3 moveDirection = new();
        UnityEvent ev = null;

        NormalizedInputMoveDirection = new();

        if (IsLeftActivated)
        {
            Vector3 dir;

            if (IsBackwardActivated)
                dir = new Vector3(-motorValues.backwardMoveSpeed, 0, 0);
            else
                dir = new Vector3(-motorValues.leftMoveSpeed, 0, 0);

            moveDirection += dir;

            NormalizedInputMoveDirection += new Vector3(-1f, 0, 0);

            ev = events.movementEvents.MovingLeft;
        }

        if (IsRightActivated)
        {
            Vector3 dir;

            if (IsBackwardActivated)
                dir = new Vector3(motorValues.backwardMoveSpeed, 0, 0);
            else
                dir = new Vector3(motorValues.rightMoveSpeed, 0, 0);

            moveDirection += dir;

            NormalizedInputMoveDirection += new Vector3(1f, 0, 0);

            ev = events.movementEvents.MovingRight;
        }

        if (IsForwardActivated)
        {
            moveDirection += new Vector3(0, 0, motorValues.forwardMoveSpeed);

            NormalizedInputMoveDirection += new Vector3(0, 0, 1f);

            ev = events.movementEvents.MovingForward;
        }

        if (IsBackwardActivated)
        {
            NormalizedInputMoveDirection += new Vector3(0, 0, -1f);

            moveDirection += new Vector3(0, 0, -motorValues.backwardMoveSpeed);
            ev = events.movementEvents.MovingBackward;
        }

        Move(transform.TransformDirection(moveDirection));
        ev?.Invoke();
    }

    private void Move(Vector3 dir)
    {
        FinalMovement += dir;
        events.Moved?.Invoke();
    }

    private void HandleGravity()
    {
        Move(-motorValues.gravity);
    }


    [System.Serializable]
    public class Events
    {
        public UnityEvent Moved = new();

        public MovementEvents movementEvents = new MovementEvents();
        public OtherEvents otherEvents = new OtherEvents();
    }

    [System.Serializable]
    public class OtherEvents
    {
        public UnityEvent Jumped = new();
        public UnityEvent Grounded = new();
    }

    [System.Serializable]
    public class MovementEvents
    {
        public UnityEvent MovingForward = new();
        public UnityEvent MovingBackward = new();
        public UnityEvent MovingLeft = new();
        public UnityEvent MovingRight = new();
        public UnityEvent Moving = new();
    }
}