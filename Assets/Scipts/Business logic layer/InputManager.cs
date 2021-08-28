using System;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;

public class InputManager
{
    public float updateTime { get; private set; }

    public ReactiveCommand OnPausePress = new ReactiveCommand();

    public ReactiveCommand<float> OnScroll = new ReactiveCommand<float>();

    public ReactiveProperty<bool> isActive = new ReactiveProperty<bool>(false);

    public bool isDownPressed { get; private set; } = false;
    public bool isLeftPressed { get; private set; } = false;
    public bool isRightPressed { get; private set; } = false;
    public bool isRotatePressed { get; private set; } = false;

    private PlayerControls playerControls = null;

    private IDisposable disposable = null;

    public InputManager(float updateTime)
    {
        this.updateTime = updateTime;

        playerControls = new PlayerControls();

        disposable = isActive.Subscribe(isActive =>
        {
            if (isActive)
            {
                Subscribe();
                playerControls.Enable();
            }
            else
            {
                Unsubscribe();
                playerControls.Disable();
            }
        });
    }

    private void Subscribe()
    {
        playerControls.KeyboardAndMouse.Approaching.performed += ctx => ProcessMouseInput(ctx);

        playerControls.KeyboardAndMouse.Pausing.performed += ctx => { OnPausePress.Execute(); };

        playerControls.KeyboardAndMouse.Movement.started += ctx => ProcessMovementInput(ctx);
        playerControls.KeyboardAndMouse.Movement.canceled += ctx => StopMovement();

        playerControls.KeyboardAndMouse.Rotation.started += ctx => { isRotatePressed = true; };
        playerControls.KeyboardAndMouse.Rotation.canceled += ctx => { isRotatePressed = false; };
    }

    private void Unsubscribe()
    {
        playerControls.KeyboardAndMouse.Approaching.performed -= ctx => ProcessMouseInput(ctx);

        playerControls.KeyboardAndMouse.Movement.started -= ctx => ProcessMovementInput(ctx);
        playerControls.KeyboardAndMouse.Movement.canceled -= ctx => StopMovement();

        playerControls.KeyboardAndMouse.Rotation.started -= ctx => { isRotatePressed = true; };
        playerControls.KeyboardAndMouse.Rotation.canceled -= ctx => { isRotatePressed = false; };
    }

    private void ProcessMovementInput(InputAction.CallbackContext ctx)
    {
        Vector2 movementVector = ctx.ReadValue<Vector2>();

        if (movementVector == Vector2.down)
        {
            isDownPressed = true;
        }

        if (movementVector == Vector2.left)
        {
            isLeftPressed = true;
        }

        if (movementVector == Vector2.right)
        {
            isRightPressed = true;
        }
    }

    private void ProcessMouseInput(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>() > 0 ? 1 : -1;
        OnScroll.Execute(value);
    }

    private void StopMovement()
    {
        isDownPressed = false;
        isLeftPressed = false;
        isRightPressed = false;
        isRotatePressed = false;
    }
}