using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerInput Input;

    [Header("Movement")]
    public RigidbodyMovement RigidbodyMovement;
    public CameraRotator CameraRotator;

    [Header("Input")]
    private InputAction moveInputAction;
    private InputAction jumpInputAction;
    private InputAction lookInputAction;

    [Header("Settings")]
    public float LookSensitivty = 2f;

    private void Awake()
    {
        Input = new PlayerInput();
        new CameraRotator.Angles();
        MapInputActions();
    }

    private void OnEnable()
    {
        Input.Enable();
    }
    private void OnDisable()
    {
        Input.Disable();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void MapInputActions()
    {
        moveInputAction = Input.Player.Movement;

        jumpInputAction = Input.Player.Jump;
        jumpInputAction.started += OnJumpInput;

        lookInputAction = Input.Player.Look;
    }
    private void Update()
    {
        var moveDirection = GetMoveDirectionFormInput();
        RigidbodyMovement.Move(moveDirection);

        var rotation = GetRotationFromInput();
        RigidbodyMovement.RotateHorizontal(rotation.x * LookSensitivty);
    }
    private void LateUpdate()
    {
        if (CameraRotator != null)
            UpdateCamera();
    }

    private void UpdateCamera()
    {
        var rotation = GetRotationFromInput();
        CameraRotator.Rotate(rotation.y);
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            RigidbodyMovement.Jump();
    }
    private Vector3 GetMoveDirectionFormInput()
    {
        var moveInput = moveInputAction.ReadValue<Vector2>();
        return new Vector3(moveInput.x, 0f, moveInput.y);
    }

    private Vector2 GetRotationFromInput()
    {
        return lookInputAction.ReadValue<Vector2>();
    }
}
