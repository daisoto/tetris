using UnityEngine;
using UniRx;

public class Block
{
    public ReactiveProperty<bool> isAlive = new ReactiveProperty<bool>(true);

    public ReactiveProperty<bool> isStuck = new ReactiveProperty<bool>(false);

    public ReactiveProperty<Vector2Int> position = new ReactiveProperty<Vector2Int>();

    public ReactiveProperty<Color> color = new ReactiveProperty<Color>();

    public ReactiveProperty<Sprite> sprite = new ReactiveProperty<Sprite>();

    public Block(bool setRandomColor = false)
    {
        if (setRandomColor)
        {
            color.Value = new Color(Random.value, Random.value, Random.value);
        }
    }

    public Block(Color color)
    {
        this.color.Value = color;
    }

    public Block(Sprite sprite)
    {
        this.sprite.Value = sprite;
    }

    public Block(Color color, Sprite sprite)
    {
        this.color.Value = color;
        this.sprite.Value = sprite;
    }
}
