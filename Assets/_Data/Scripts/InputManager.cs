using System;
using UnityEngine;

public class InputManager : NhoxBehaviour
{
    [SerializeField] protected bool isUIMode;

    [SerializeField] protected float inputHoldTime = 0.2f;

    [SerializeField] protected Camera cam;

    [SerializeField] private Transform player;
    protected float dashInputStartTime;

    protected bool isPaused;
    protected float jumpInputStartTime;
    public static InputManager Instance { get; private set; }

    public bool InteractInput { get; private set; }

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    public bool GrabInput { get; private set; }

    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }

    public bool[] AttackInputs { get; private set; }

    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Debug.LogError("InputManager already exist...");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    protected override void Start()
    {
        var count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    private void Update()
    {
        ProcessInteractInput();

        if (isUIMode || isPaused) return;

        ProcessMovementInput();
        ProcessJumpInput();
        ProcessGrabInput();
        ProcessDashDirectionInput();
        ProcessDashInput();
        ProcessAttackInput();

        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public event Action<bool> OnInteractInputChanged;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCamera();
        LoadPlayer();
    }

    protected void LoadCamera()
    {
        if (cam != null) return;
        cam = Camera.main;
        Debug.Log(transform.name + " LoadCamera", gameObject);
    }

    protected void LoadPlayer()
    {
        if (player != null) return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(transform.name + " LoadPlayer", gameObject);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SetUIMode(bool isUI)
    {
        isUIMode = isUI;
        if (isUI)
            ResetInputs();
    }

    protected void ProcessMovementInput()
    {
        var leftKey = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        var rightKey = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        var upKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        var downKey = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        var horizontal = 0f;
        var vertical = 0f;

        if (leftKey && !rightKey)
            horizontal = -1f;
        else if (rightKey && !leftKey)
            horizontal = 1f;

        if (upKey && !downKey)
            vertical = 1f;
        else if (downKey && !upKey)
            vertical = -1f;

        RawMovementInput = new Vector2(horizontal, vertical);
        NormInputX = Mathf.RoundToInt(horizontal);
        NormInputY = Mathf.RoundToInt(vertical);
    }

    protected void ProcessJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space)) JumpInputStop = true;
    }

    protected void ProcessGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
            GrabInput = true;
        if (Input.GetKeyUp(KeyCode.E))
            GrabInput = false;
    }

    protected void ProcessDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) DashInputStop = true;
    }

    protected void ProcessDashDirectionInput()
    {
        var mousePos = Input.mousePosition;
        var worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        Vector2 dashDirection = worldPos - player.position;
        RawDashDirectionInput = dashDirection;
        DashDirectionInput = Vector2Int.RoundToInt(dashDirection.normalized);
    }

    protected void ProcessAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
            AttackInputs[(int)CombatInputs.primary] = true;
        if (Input.GetMouseButtonUp(0))
            AttackInputs[(int)CombatInputs.primary] = false;

        if (Input.GetMouseButtonDown(1))
            AttackInputs[(int)CombatInputs.secondary] = true;
        if (Input.GetMouseButtonUp(1))
            AttackInputs[(int)CombatInputs.secondary] = false;
    }

    protected void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime) JumpInput = false;
    }

    protected void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime) DashInput = false;
    }

    private void ProcessInteractInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractInput = true;
            OnInteractInputChanged?.Invoke(true);
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            InteractInput = false;
            OnInteractInputChanged?.Invoke(false);
        }
    }

    public void UseAttackInput(int attackIndex)
    {
        AttackInputs[attackIndex] = false;
    }

    public void UseJumpInput()
    {
        JumpInput = false;
    }

    public void UseDashInput()
    {
        DashInput = false;
    }

    public void ResetInputs()
    {
        RawMovementInput = Vector2.zero;
        NormInputX = 0;
        NormInputY = 0;
        JumpInput = false;
        JumpInputStop = false;
        GrabInput = false;
        DashInput = false;
        DashInputStop = false;
        AttackInputs[(int)CombatInputs.primary] = false;
        AttackInputs[(int)CombatInputs.secondary] = false;
        RawDashDirectionInput = Vector2.zero;
        DashDirectionInput = Vector2Int.zero;

        InteractInput = false;
        OnInteractInputChanged?.Invoke(false);
    }

    public void Pause()
    {
        isPaused = true;
        ResetInputs();
    }

    public void Unpause()
    {
        isPaused = false;
    }
}

public enum CombatInputs
{
    primary,
    secondary
}