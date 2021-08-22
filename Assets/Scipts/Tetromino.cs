using UnityEngine;
using UniRx;

public class Tetromino : ITetromino
{
    public Block[] blocks = null;

    public ReactiveProperty<Vector2Int> currentAxis = new ReactiveProperty<Vector2Int>();

    private IRotator rotator = null;

    private IMover tickMover = null;

    private IMover mover = null;

    public Tetromino(Block[] blocks, IRotator rotator, IMover tickMover)
    {
        this.blocks = blocks;
        this.rotator = rotator;
        this.tickMover = tickMover;
    }

    public void Rotate()
    {
        foreach (Block block in blocks)
        {
            block.position.Value = rotator.GetRotatedPosition(block.position.Value, currentAxis.Value);
        }
    }

    public void TickMove() // Вынести в отдельный класс ???
    {
        foreach (Block block in blocks)
        {
            if (!block.isStuck.Value)
            {
                block.position.Value = tickMover.GetMovedPosition(block.position.Value);
            }
        }        
    }

    public void SetMover(IMover mover)
    {
        if (this.mover == mover)
        {
            return;
        }

        this.mover = mover;
    }

    public void Move()
    {
        foreach (Block block in blocks)
        {
            block.position.Value = mover.GetMovedPosition(block.position.Value);
        }
    }
}
