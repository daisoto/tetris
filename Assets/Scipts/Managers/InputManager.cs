using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : ConstructableBehaviour<float>
{
    public ReactiveCommand onDownPressed = new ReactiveCommand();
    public ReactiveCommand onLeftPressed = new ReactiveCommand();
    public ReactiveCommand onRightPressed = new ReactiveCommand();
    public ReactiveCommand onRotatePressed = new ReactiveCommand();

    public ReactiveCommand<float> onScroll = new ReactiveCommand<float>();

    public ReactiveProperty<bool> isActive = new ReactiveProperty<bool>();

    private bool isDownPressed = false;
    private bool isLeftPressed = false;
    private bool isRightPressed = false;
    private bool isRotatePressed = false;

    private PlayerControls playerControls = null;

    private IEnumerator inputCoroutine = null;

    private IDisposable isActiveSubscription = null;

    public override void Construct(float updateTime)
    {
        playerControls = new PlayerControls();
        inputCoroutine = UpdateInput();

        model = updateTime;

        isConstructed = true;
    }

    protected override void Subscribe()
    {
        playerControls.KeyboardAndMouse.Approaching.performed += ctx => ProcessMouseInput(ctx);

        playerControls.KeyboardAndMouse.Movement.started += ctx => ProcessMovementInput(ctx);
        playerControls.KeyboardAndMouse.Movement.canceled += ctx => StopMovement();

        playerControls.KeyboardAndMouse.Rotation.started += ctx => { isRotatePressed = true; };
        playerControls.KeyboardAndMouse.Rotation.canceled += ctx => { isRotatePressed = false; };

        isActiveSubscription = isActive.Subscribe(isActive =>
        {
            if (isActive)
            {
                playerControls.Enable();
                StartCoroutine(inputCoroutine);
            }
            else
            {
                playerControls.Disable();
                StopCoroutine(inputCoroutine);
            }
        });
    }

    protected override void Unsubscribe()
    {
        playerControls.KeyboardAndMouse.Approaching.performed -= ctx => ProcessMouseInput(ctx);

        playerControls.KeyboardAndMouse.Movement.started -= ctx => ProcessMovementInput(ctx);
        playerControls.KeyboardAndMouse.Movement.canceled -= ctx => StopMovement();

        playerControls.KeyboardAndMouse.Rotation.started -= ctx => { isRotatePressed = true; };
        playerControls.KeyboardAndMouse.Rotation.canceled -= ctx => { isRotatePressed = false; };

        StopCoroutine(inputCoroutine);

        isActiveSubscription.Dispose();
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

    private void StopMovement()
    {
        isDownPressed = false;
        isLeftPressed = false;
        isRightPressed = false;
        isRotatePressed = false;
    }

    private void ProcessMouseInput(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>() > 0 ? 1 : -1;
        onScroll.Execute(value);
    }

    private IEnumerator UpdateInput()
    {
        while (isActive.Value)
        {
            if (isDownPressed)
            {
                onDownPressed?.Execute();
            }

            if (isLeftPressed)
            {
                onLeftPressed?.Execute();
            }

            if (isRightPressed)
            {
                onRightPressed?.Execute();
            }

            if (isRotatePressed)
            {
                onRotatePressed?.Execute();
            }

            yield return new WaitForSeconds(model);
        }
    }
}