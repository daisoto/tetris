using UnityEngine;

public class DownBlocksMover : BlocksMover
{
    public override Vector2Int moveVector { get => Vector2Int.down; }

    public DownBlocksMover(bool[,] grid) : base(grid) { }

    public override void MoveDefault(Block[] blocks)
    {
        MoveDown(blocks);
    }

    public override void MoveDefault(Block block)
    {
        MoveDown(block);
    }
}
