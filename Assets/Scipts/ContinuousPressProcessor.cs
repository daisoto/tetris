using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class ContinuousPressProcessor: MonoBehaviour, IConstructable<float>
{
    public ReactiveCommand OnPressed = new ReactiveCommand();

    public ReactiveProperty<bool> isPressed = new ReactiveProperty<bool>();

    public bool isConstructed { get; private set; }

    private IEnumerator coroutine = null;

    private IDisposable disposable = null;

    private float updateTime = default;

    public void Construct(float updateTime)
    {
        coroutine = ContinuousExecution();
        this.updateTime = updateTime;

        isConstructed = true;

        OnEnable();
    }

    private void OnEnable()
    {
        if (isConstructed)
        {
            disposable = isPressed.Subscribe(isPressed =>
            {
                if (isPressed)
                {
                    StartCoroutine(coroutine);
                }
                else
                {
                    StopCoroutine(coroutine);
                }
            });
        }
    }

    private void OnDisable()
    {
        disposable?.Dispose();
    }

    private IEnumerator ContinuousExecution()
    {
        while (isPressed.Value)
        {
            OnPressed.Execute();
            yield return new WaitForSeconds(updateTime);
        }
    }
}