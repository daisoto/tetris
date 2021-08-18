using UnityEngine;
using UniRx;

public class Block
{
    public ReactiveProperty<bool> isAlive = new ReactiveProperty<bool>(true);

    public ReactiveProperty<bool> isStuck = new ReactiveProperty<bool>(false);

    public ReactiveProperty<Vector2Int> position = new ReactiveProperty<Vector2Int>();

    public ReactiveProperty<Color> color = new ReactiveProperty<Color>();
}
