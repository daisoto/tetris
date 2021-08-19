public interface IConstructable<T>
{
    bool isConstructed { get; }

    void Construct(T model);
}