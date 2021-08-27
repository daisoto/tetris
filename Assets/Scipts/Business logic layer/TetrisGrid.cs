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

    public void Rotate(Tetromino tetromino)
    {
        blocksRotator.Rotate(tetromino.blocks);
    }

    public void MoveDefault(Tetromino tetromino)
    {
        foreach (Block block in tetromino.blocks)
        {
            if (!gridChecker.IsDefaultSpaceFree(block.position.Value))
            {
                Insert(tetromino);
                return;
            }
        }

        blocksMover.MoveDefault(tetromino.blocks);
    }

    public void MoveLeft(Tetromino tetromino)
    {
        foreach (Block block in tetromino.blocks)
        {
            if (!gridChecker.IsLeftSpaceFree(block.position.Value))
            {
                return;
            }
        }

        blocksMover.MoveLeft(tetromino.blocks);
    }

    public void MoveRight(Tetromino tetromino)
    {
        foreach (Block block in tetromino.blocks)
        {
            if (!gridChecker.IsRightSpaceFree(block.position.Value))
            {
                return;
            }
        }

        blocksMover.MoveRight(tetromino.blocks);
    }

    public void MoveDown(Tetromino tetromino)
    {
        foreach (Block block in tetromino.blocks)
        {
            if (!gridChecker.IsDownSpaceFree(block.position.Value))
            {            
                return;
            }
        }

        blocksMover.MoveDown(tetromino.blocks);
    }

    public void Insert(Tetromino tetromino)
    {
        foreach (Block block in tetromino.blocks)
        {
            Vector2Int position = block.position.Value;

            if (positionStuckBlocks.ContainsKey(position))
            {
                block.isAlive.Value = false;

                onGameOver.Execute();

                return;
            }

            block.isStuck.Value = true;

            positionStuckBlocks.Add(position, block);

            grid[position.x, position.y] = true;
        }

        ProcessInsertedBlocks(tetromino.blocks);
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
