using UniRx;
using UnityEngine;

public class TetrominoBehaviour : MonoBehaviour, ITetromino
{
    private ITetromino tetromino = null;

    public ReactiveProperty<Vector2Int> currentAxis => throw new System.NotImplementedException();

    private bool isConstructed = false;

    public void Construct(ITetromino tetromino)
    {
        this.tetromino = tetromino;

        isConstructed = true;
    }

    public void Move()
    {
        if (isConstructed)
        {
            tetromino.Move();
        }
    }

    public void Rotate()
    {
        if (isConstructed)
        {
            tetromino.Rotate();
        }
    }

    public void TickMove()
    {
        if (isConstructed)
        {
            tetromino.TickMove();
        }
    }
}
