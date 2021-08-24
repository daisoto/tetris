using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class InputManager: MonoBehaviour, IConstructable<float>
{
    public bool isConstructed { get; private set; }

    public ReactiveCommand OnDownPressed => downContinuousPressProcessor.OnPressed;
    public ReactiveCommand OnLeftPressed => leftContinuousPressProcessor.OnPressed;
    public ReactiveCommand OnRightPressed => rightContinuousPressProcessor.OnPressed;
    public ReactiveCommand OnRotatePressed => rotateContinuousPressProcessor.OnPressed;

    private ContinuousPressProcessor downContinuousPressProcessor = null;
    private ContinuousPressProcessor leftContinuousPressProcessor = null;
    private ContinuousPressProcessor rightContinuousPressProcessor = null;
    private ContinuousPressProcessor rotateContinuousPressProcessor = null;

    private PlayerControls playerControls = null;

    private float updateTime = 0.1f;

    public void Construct(float updateTime)
    {
        this.updateTime = updateTime;

        playerControls = new PlayerControls();

        CreatePressProcessors();

        isConstructed = transform;

        OnEnable();
    }

    private void OnEnable()
    {
        if (isConstructed)
        {
            playerControls.Keyboard.Movement.started += ctx => ProcessMovementInput(ctx);
            playerControls.Keyboard.Movement.canceled += ctx => StopMovementPressProcessors();

            playerControls.Keyboard.Rotation.started += ctx => { rotateContinuousPressProcessor.isPressed.Value = true; };
            playerControls.Keyboard.Rotation.canceled += ctx => { rotateContinuousPressProcessor.isPressed.Value = false; };
        }
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Keyboard.Movement.started -= ctx => ProcessMovementInput(ctx);
            playerControls.Keyboard.Movement.canceled -= ctx => StopMovementPressProcessors();

            playerControls.Keyboard.Rotation.started -= ctx => { rotateContinuousPressProcessor.isPressed.Value = true; };
            playerControls.Keyboard.Rotation.canceled -= ctx => { rotateContinuousPressProcessor.isPressed.Value = false; };
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
            downContinuousPressProcessor.isPressed.Value = true;
        }

        if (movementVector == Vector2.left)
        {
            leftContinuousPressProcessor.isPressed.Value = true;
        }

        if (movementVector == Vector2.right)
        {
            rightContinuousPressProcessor.isPressed.Value = true;
        }
    }

    private void CreatePressProcessors()
    {
        downContinuousPressProcessor = gameObject.AddComponent<ContinuousPressProcessor>();
        downContinuousPressProcessor.Construct(updateTime);

        leftContinuousPressProcessor = gameObject.AddComponent<ContinuousPressProcessor>();
        leftContinuousPressProcessor.Construct(updateTime);

        rightContinuousPressProcessor = gameObject.AddComponent<ContinuousPressProcessor>();
        rightContinuousPressProcessor.Construct(updateTime);

        rotateContinuousPressProcessor = gameObject.AddComponent<ContinuousPressProcessor>();
        rotateContinuousPressProcessor.Construct(updateTime);
    }

    private void StopMovementPressProcessors()
    {
        downContinuousPressProcessor.isPressed.Value = false;
        leftContinuousPressProcessor.isPressed.Value = false;
        rightContinuousPressProcessor.isPressed.Value = false;
    }
}