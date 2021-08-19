using UniRx;
using UnityEngine;

public class TetrominoBehaviour : ConstructableBehaviour<ITetromino>, ITetromino
{
    public ReactiveProperty<Vector2Int> currentAxis => throw new System.NotImplementedException();

    public void Move() // TODO : Возможно совсем уберу т.к. буду вызывать их непосредственно у модели, а эту шляпу онли для инстанциализации в игре
    {
        if (isConstructed)
        {
            model.Move();
        }
    }

    public void Rotate()
    {
        if (isConstructed)
        {
            model.Rotate();
        }
    }

    public void TickMove()
    {
        if (isConstructed)
        {
            model.TickMove();
        }
    }
}
