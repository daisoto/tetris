using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DownGridCleanerProcessor : IGridCleanProcessor
{
    private bool[,] grid = null;
    private Vector2Int gridSize = default;
    private IGridChecker gridChecker = null;
    private IBlocksMover blocksMover = null;

    public DownGridCleanerProcessor(bool[,] grid, IGridChecker gridChecker, IBlocksMover blocksMover)
    {
        this.grid = grid;
        gridSize = new Vector2Int(grid.GetLength(0), grid.GetLength(1));

        this.gridChecker = gridChecker;
        this.blocksMover = blocksMover;
    }

    public void ProcessAfterCleaning(Dictionary<Vector2Int, Block> positionStuckBlocks, Vector2Int[] cleanedPositions)
    {
        int yCleanMax = cleanedPositions.Select(position => position.y).ToArray().Max();

        for (int y = yCleanMax + 1; y < gridSize.y; y++)
        { 
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2Int position = new Vector2Int(x, y);

                if (positionStuckBlocks.ContainsKey(position) && gridChecker.IsDefaultSpaceFree(position))
                {
                    Block block = positionStuckBlocks[position];
                    grid[position.x, position.y] = false;
                    positionStuckBlocks.Remove(position);
                    blocksMover.MoveDefault(block);

                    Vector2Int newPosition = block.position.Value;
                    grid[newPosition.x, newPosition.y] = true;
                    positionStuckBlocks.Add(newPosition, block);
                }
            }
        }
    }
}