using System.Collections;
using UniRx;
using UnityEngine;

public class InputManagerBehaviour : ConstructableBehaviour<InputManager>
{
    public ReactiveCommand OnDownPress = new ReactiveCommand();
    public ReactiveCommand OnLeftPress = new ReactiveCommand();
    public ReactiveCommand OnRightPress = new ReactiveCommand();
    public ReactiveCommand OnRotatePress = new ReactiveCommand();

    public ReactiveCommand OnPausePress = new ReactiveCommand();

    public ReactiveCommand<float> OnScroll = new ReactiveCommand<float>();

    private IEnumerator inputCoroutine = null;

    public override void Construct(InputManager inputManager)
    {
        model = inputManager;

        inputCoroutine = UpdateInput();

        isConstructed = true;
    }

    protected override void Subscribe()
    {
        disposablesContainer.Add(model.isActive.Subscribe(isActive =>
        {
            if (isActive)
            {
                StartCoroutine(inputCoroutine);
            }
            else
            {
                StopCoroutine(inputCoroutine);
            }
        }));

        disposablesContainer.Add(model.OnPausePress.Subscribe(_ =>
        {
            OnPausePress.Execute();
        }));

        disposablesContainer.Add(model.OnScroll.Subscribe(scrollValue =>
        {
            OnScroll.Execute(scrollValue);
        }));
    }

    protected override void Unsubscribe()
    {
        StopCoroutine(inputCoroutine);

        base.Unsubscribe();
    }  

    private IEnumerator UpdateInput()
    {
        while (model.isActive.Value)
        {
            yield return new WaitForSeconds(model.updateTime);

            if (model.isDownPressed)
            {
                OnDownPress?.Execute();
            }

            if (model.isLeftPressed)
            {
                OnLeftPress?.Execute();
            }

            if (model.isRightPressed)
            {
                OnRightPress?.Execute();
            }

            if (model.isRotatePressed)
            {
                OnRotatePress?.Execute();
            }
        }
    }
}