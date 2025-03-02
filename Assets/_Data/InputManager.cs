using UnityEngine;

public class InputManager : NhoxBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    public float HorizontalInput { get; private set; }
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
        bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool upPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool downPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (leftPressed && !rightPressed)
        {
            HorizontalInput = -1f;
        }
        else if (rightPressed && !leftPressed)
        {
            HorizontalInput = 1f;
        }
        else
        {
            HorizontalInput = 0f;
        }
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
        JumpPressed = false;
        JumpHeld = false;
        JumpReleased = false;
        DashPressed = false;
        AttackPressed = false;
        InteractPressed = false;
    }
}