using System.Collections.Generic;
using System;

public class DisposablesContainer
{
    private List<IDisposable> disposables = null;

    ~DisposablesContainer()
    {
        Clear();
    }

    public void Add(IDisposable disposable)
    {
        disposables.Add(disposable);
    }

    public void Clear()
    {
        disposables.ForEach(disposable => disposable.Dispose());
        disposables.Clear();
    }
}