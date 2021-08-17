using UnityEngine;
using UniRx;

public class Tetramine : ITetramine
{
    public ReactiveProperty<Quaternion> currentRotation { get; private set; } = new ReactiveProperty<Quaternion>();

    public ReactiveProperty<Vector2Int> currentCoordinates { get; private set; } = new ReactiveProperty<Vector2Int>();

    public Tetramine() // TODO: Задаем форму, начальные координаты и вращение
    { 
    }

    public void Rotate(Quaternion rotation)
    {
        // TODO: поворот, смена координат
    }

    public void Move()
    { 
    }
}
