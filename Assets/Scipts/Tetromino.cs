using UnityEngine;
using UniRx;

public class Tetromino
{
    public Block[] blocks { get; private set; }

    public ReactiveProperty<Vector2Int> currentAxis = new ReactiveProperty<Vector2Int>();

    private IRotator rotator = null;

    private IMover mover = null;

    public Tetromino(Block[] blocks)
    {
        this.blocks = blocks;
    }

    public void Rotate()
    {
        foreach (Block block in blocks)
        { 
            block.position.Value = rotator.GetRotatedPosition(block.position.Value, currentAxis.Value);
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

    public void SetRotator(IRotator rotator)
    {
        if (this.rotator == rotator)
        {
            return;
        }

        this.rotator = rotator;
    }

    public void Move()
    {
        foreach (Block block in blocks)
        {
            block.position.Value = mover.GetMovedPosition(block.position.Value);
        }
    }
}
