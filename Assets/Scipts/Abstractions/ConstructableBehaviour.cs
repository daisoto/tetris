using UnityEngine;

public abstract class ConstructableBehaviour<T> : MonoBehaviour, IConstructable<T> where T: class
{
    public bool isConstructed { get => _isConstructed; }

    protected T model = null;

    protected bool _isConstructed = false;

    protected DisposablesContainer disposablesContainer = new DisposablesContainer();

    public virtual void Construct(T model)
    {
        this.model = model;

        _isConstructed = true;

        OnEnable();
    }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable()
    {
        disposablesContainer.Clear();
    }
}
