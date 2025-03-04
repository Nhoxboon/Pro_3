using UnityEngine;

public class InputManager : NhoxBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    public float HorizontalInput { get; private set; }
    public bool HorizontalButtonPressed { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpHeld { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool DashPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool PausePressed { get; private set; }

    [Header("Action Keys")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("InputManager already exists in the scene. Deleting duplicate...");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        ProcessMovementInput();
        ProcessJumpInput();
        ProcessActionInput();
    }

    private void ProcessMovementInput()
    {
        bool leftKeyHeld = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightKeyHeld = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (leftKeyHeld && !rightKeyHeld)
        {
            HorizontalInput = -1f;
        }
        else if (rightKeyHeld && !leftKeyHeld)
        {
            HorizontalInput = 1f;
        }
        else
        {
            HorizontalInput = 0f;
        }

        HorizontalButtonPressed = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                                  Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }

    private void ProcessJumpInput()
    {
        JumpPressed = Input.GetKeyDown(jumpKey) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        JumpHeld = Input.GetKey(jumpKey) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        JumpReleased = Input.GetKeyUp(jumpKey) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
    }

    private void ProcessActionInput()
    {
        DashPressed = Input.GetKeyDown(dashKey);
        AttackPressed = Input.GetMouseButtonDown(0); // 0 = left mouse button
        InteractPressed = Input.GetKeyDown(interactKey);
        PausePressed = Input.GetKeyDown(pauseKey);
    }

    public void ResetInputs()
    {
        HorizontalInput = 0f;
        HorizontalButtonPressed = false;
        JumpPressed = false;
        JumpHeld = false;
        JumpReleased = false;
        DashPressed = false;
        AttackPressed = false;
        InteractPressed = false;
        PausePressed = false;
    }
}
