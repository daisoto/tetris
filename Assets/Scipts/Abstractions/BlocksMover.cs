using UnityEngine;

public abstract class BlocksMover : IBlocksMover
{
    protected bool[,] grid = null;

    public BlocksMover(bool[,] grid)
    {
        this.grid = grid;
    }

    public abstract void MoveDefault(Block[] blocks);

    public abstract void MoveDefault(Block block);

    public void MoveDown(Block[] blocks)
    {
        foreach (Block block in blocks)
        {
            block.position.Value += Vector2Int.down;
        }
    }

    public void MoveDown(Block block)
    {
        block.position.Value += Vector2Int.down;
    }

    public void MoveLeft(Block[] blocks)
    {
        foreach (Block block in blocks)
        {
            block.position.Value += Vector2Int.left;
        }
    }

    public void MoveLeft(Block block)
    {
        block.position.Value += Vector2Int.left;
    }

    public void MoveRight(Block[] blocks)
    {
        foreach (Block block in blocks)
        {
            block.position.Value += Vector2Int.right;
        }
    }

    public void MoveRight(Block block)
    {
        block.position.Value += Vector2Int.right;
    }
}