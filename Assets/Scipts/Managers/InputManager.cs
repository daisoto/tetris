using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : ITickable
{
    public ReactiveCommand onDownPressed = new ReactiveCommand();
    public ReactiveCommand onLeftPressed = new ReactiveCommand();
    public ReactiveCommand onRightPressed = new ReactiveCommand();
    public ReactiveCommand onRotatePressed = new ReactiveCommand();

    private bool isDownPressed = false;
    private bool isLeftPressed = false;
    private bool isRightPressed = false;
    private bool isRotatePressed = false;

    private PlayerControls playerControls = null;

    public InputManager()
    {
        playerControls = new PlayerControls();

        Subscribe();
    }

    private void Subscribe()
    {
        playerControls.Keyboard.Movement.started += ctx => ProcessMovementInput(ctx);
        playerControls.Keyboard.Movement.canceled += ctx => StopMovement();

        playerControls.Keyboard.Rotation.started += ctx => { isRotatePressed = true; };
        playerControls.Keyboard.Rotation.canceled += ctx => { isRotatePressed = false; };
    }

    ~InputManager()
    {
        if (playerControls != null)
        {
            playerControls.Keyboard.Movement.started -= ctx => ProcessMovementInput(ctx);
            playerControls.Keyboard.Movement.canceled -= ctx => StopMovement();

            playerControls.Keyboard.Rotation.started -= ctx => { isRotatePressed = true; };
            playerControls.Keyboard.Rotation.canceled -= ctx => { isRotatePressed = false; };
        }
    }

    public void Tick()
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
    }

    public void SetActive(bool flag)
    {
        if (flag)
        {
            playerControls?.Enable();
        }
        else
        {
            playerControls?.Disable();
        }
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
}