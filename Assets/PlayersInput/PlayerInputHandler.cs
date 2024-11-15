using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "jump";
    [SerializeField] private string sprint = "sprint";
    [SerializeField] private string grappling = "grappling";
    [SerializeField] private string shoot = "shoot";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction grabAction;
    private InputAction shootAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool GrabTriggered { get; set; }
    public bool ShootTriggered { get; set; }
    public float SprintValue { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        grabAction = playerControls.FindActionMap(actionMapName).FindAction(grappling);
        shootAction = playerControls.FindActionMap(actionMapName).FindAction(shoot);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => MoveInput = Vector2.zero;

        lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => LookInput = Vector2.zero;
        
        jumpAction.performed += ctx => JumpTriggered = true;
        jumpAction.canceled += ctx => JumpTriggered = false;
        
        sprintAction.performed += ctx => SprintValue = ctx.ReadValue<float>();
        sprintAction.canceled += ctx => SprintValue = 0f;

        grabAction.performed += ctx => GrabTriggered = true;
        grabAction.canceled += ctx => GrabTriggered = false;

        shootAction.performed += ctx => ShootTriggered = true;
        shootAction.canceled += ctx => ShootTriggered = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        grabAction.Enable();
        shootAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        grabAction.Disable();
        shootAction.Disable();
    }
}
