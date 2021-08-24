using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TetrisGrid
{
    public ReactiveCommand<int> OnBlocksClear = new ReactiveCommand<int>();
    public ReactiveCommand<Block[]> OnInsert = new ReactiveCommand<Block[]>();

    private bool[,] grid = null;

    private IGridChecker gridChecker = null;

    private IBlocksMover blocksMover = null;

    private IBlocksRotator blocksRotator = null;

    private Dictionary<Vector2Int, Block> positionStuckBlocks = new Dictionary<Vector2Int, Block>();

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    private List<Block> movingBlocks = new List<Block>();

    public TetrisGrid(bool[,] grid, IGridChecker gridChecker, IBlocksMover blocksMover, IBlocksRotator blocksRotator)
    {
        this.grid = grid;
        this.gridChecker = gridChecker;
        this.blocksMover = blocksMover;
        this.blocksRotator = blocksRotator;

        disposablesContainer.Add(OnInsert.Subscribe(blocks =>
        {
            ProcessInsertedBlocks(blocks);
        }));
    }

    public void AddBlocks(Block[] blocks)
    {
        movingBlocks.AddRange(blocks);
    }

    public void Rotate(Block[] blocks)
    {
        blocksRotator.Rotate(blocks);
    }

    public void DefaultMove(Block[] blocks)
    {
        foreach (Block block in blocks)
        {
            if (!gridChecker.IsDefaultSpaceFree(block.position.Value))
            {
                Insert(blocks);
                return;
            }
        }

        blocksMover.MoveDefault(blocks);
    }

    public void MoveLeft(Block[] blocks)
    {
        foreach (Block block in blocks)
        {
            if (!gridChecker.IsLeftSpaceFree(block.position.Value))
            {
                return;
            }
        }

        blocksMover.MoveLeft(blocks);
    }

    public void MoveRight(Block[] blocks)
    {
        foreach (Block block in blocks)
        {
            if (!gridChecker.IsDownSpaceFree(block.position.Value))
            {
                return;
            }
        }

        blocksMover.MoveRight(blocks);
    }

    public void MoveDown(Block[] blocks)
    {
        foreach (Block block in blocks)
        {
            if (!gridChecker.IsDownSpaceFree(block.position.Value))
            {            
                return;
            }
        }

        blocksMover.MoveDown(blocks);
    }

    public void Insert(Block[] blocks)
    {
        foreach (Block block in blocks)
        {
            block.isStuck.Value = true;
            Vector2Int position = block.position.Value;
            positionStuckBlocks.Add(position, block);
            grid[position.x, position.y] = true;
        }

        OnInsert.Execute(blocks);
    }    

    private void ProcessInsertedBlocks(IEnumerable<Block> blocks)
    {
        int rawScore = 0;
        List<Vector2Int> positionsToClear = new List<Vector2Int>();

        foreach (Block block in blocks)
        {
            if (gridChecker.IsRowColumnFilled(block.position.Value, out Vector2Int[] newPositionsToClear))
            {
                positionsToClear.AddRange(newPositionsToClear);
                rawScore++;
            }
        }

        if (rawScore > 0)
        {
            ClearBlocks(positionsToClear.ToArray());
            ProcessAfterCleaning();

            OnBlocksClear.Execute(rawScore);
        }
    }

    private void ClearBlocks(Vector2Int[] positionsToClear)
    {
        foreach (Vector2Int positionToClear in positionsToClear)
        {
            positionStuckBlocks[positionToClear].isAlive.Value = false;
            positionStuckBlocks.Remove(positionToClear);
            grid[positionToClear.x, positionToClear.y] = false;
        }        
    }

    private void ProcessAfterCleaning()
    {
        foreach (Vector2Int position in positionStuckBlocks.Keys)
        {
            if (gridChecker.IsDefaultSpaceFree(position))
            {
                blocksMover.MoveDefault(positionStuckBlocks[position]);
            }
        }
    }
}
