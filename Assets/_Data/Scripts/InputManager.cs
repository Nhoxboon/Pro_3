using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : NhoxBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    public bool InteractInput { get; private set; }
    public event System.Action<bool> OnInteractInputChanged;

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    protected float jumpInputStartTime;

    public bool GrabInput { get; private set; }

    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    protected float dashInputStartTime;

    public bool[] AttackInputs { get; private set; }

    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }

    [SerializeField] protected float inputHoldTime = 0.2f;

    [SerializeField] protected Camera cam;

    [SerializeField] private Transform player;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("InputManager already exist...");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    protected override void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    private void Update()
    {
        ProcessMovementInput();
        ProcessJumpInput();
        ProcessGrabInput();
        ProcessDashDirectionInput();
        ProcessDashInput();
        ProcessAttackInput();
        ProcessInteractInput();

        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCamera();
        LoadPlayer();
    }

    protected void LoadCamera()
    {
        if (this.cam != null) return;
        this.cam = Camera.main;
        Debug.Log(transform.name + " LoadCamera", gameObject);
    }

    protected void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(transform.name + " LoadPlayer", gameObject);
    }

    protected void ProcessMovementInput()
    {
        bool leftKey = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightKey = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool upKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool downKey = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        float horizontal = 0f;
        float vertical = 0f;

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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpInputStop = true;
        }
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
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DashInputStop = true;
        }
    }

    protected void ProcessDashDirectionInput()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        Vector2 dashDirection = (worldPos - player.position);
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
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    protected void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
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

    public void UseAttackInput(int attackIndex) => AttackInputs[attackIndex] = false;

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;

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
}

public enum CombatInputs
{
    primary,
    secondary
}
