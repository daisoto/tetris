using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class TetrisGrid
{
    public ReactiveCommand onGameOver = new ReactiveCommand();

    public ReactiveCommand<int> OnBlocksClear = new ReactiveCommand<int>();

    private bool[,] grid = null;

    private IGridChecker gridChecker = null;

    private IBlocksMover blocksMover = null;

    private IBlocksRotator blocksRotator = null;

    private IGridCleanProcessor gridCleanProcessor = null;

    private Dictionary<Vector2Int, Block> positionStuckBlocks = new Dictionary<Vector2Int, Block>();

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    private List<Block> movingBlocks = new List<Block>();

    public TetrisGrid(bool[,] grid)
    {
        this.grid = grid;

        gridChecker = new DownGridChecker(grid);
        blocksMover = new DownBlocksMover(grid);
        blocksRotator = new RightAngleBlocksRotator(gridChecker);
        gridCleanProcessor = new DownGridCleanerProcessor(grid, gridChecker, blocksMover);
    }

    public void ClearGrid()
    {
        foreach (KeyValuePair<Vector2Int, Block> positionStuckBlock in positionStuckBlocks)
        {
            Vector2Int position = positionStuckBlock.Key;
            grid[position.x, position.y] = false;
            positionStuckBlock.Value.isAlive.Value = false;
        }

        positionStuckBlocks.Clear();
    }

    public void AddBlocks(Block[] blocks)
    {
        movingBlocks.AddRange(blocks);
    }

    public void Rotate(Block[] blocks)
    {
        blocksRotator.Rotate(blocks);
    }

    public void MoveDefault(Block[] blocks)
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
            if (!gridChecker.IsRightSpaceFree(block.position.Value))
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

            if (positionStuckBlocks.ContainsKey(position))
            {
                block.isAlive.Value = false;

                onGameOver.Execute();

                return;
            }

            positionStuckBlocks.Add(position, block);

            grid[position.x, position.y] = true;
        }

        ProcessInsertedBlocks(blocks);
    }

    private void ProcessInsertedBlocks(Block[] blocks)
    {
        Vector2Int[] blocksPositios = blocks.Select(x => x.position.Value).ToArray();
        int rawScore = gridChecker.GetScoreFromFilled(blocksPositios, out Vector2Int[] positionsToClear);

        if (rawScore > 0)
        {
            ClearBlocks(positionsToClear);
            gridCleanProcessor.ProcessAfterCleaning(positionStuckBlocks, positionsToClear);

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
}
