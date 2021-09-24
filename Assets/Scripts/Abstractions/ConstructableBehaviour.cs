using UnityEngine;

public abstract class ConstructableBehaviour<T> : MonoBehaviour, IConstructable<T>
{
    public bool isConstructed { get; protected set; }

    protected T model = default;

    protected DisposablesContainer disposablesContainer = new DisposablesContainer();

    public virtual void Construct(T model)
    {
        if (isConstructed)
        {
            return;
        }

        this.model = model;

        isConstructed = true;

        OnEnable();
    }

    protected virtual void OnEnable() 
    {
        if (isConstructed)
        {
            Subscribe();
        }
    }

    protected virtual void OnDisable()
    {
        if (isConstructed)
        {
            Unsubscribe();
        }
    }

    protected virtual void Subscribe() { }

    protected virtual void Unsubscribe()
    {
        disposablesContainer?.Clear();
    }
}
