using UnityEngine;

public abstract class GridChecker : IGridChecker
{
    protected bool[,] grid = null;
    protected Vector2Int gridSize = default;

    public GridChecker(bool[,] grid)
    {
        this.grid = grid;
        gridSize = new Vector2Int(grid.GetLength(0), grid.GetLength(1));
    }

    public abstract bool IsDefaultSpaceFree(Vector2Int position);

    public abstract int GetScoreFromFilled(Vector2Int[] insertedPositions, out Vector2Int[] positionsToClear);

    public virtual bool IsDownSpaceFree(Vector2Int position)
    {
        Vector2Int newPosition = new Vector2Int(position.x, position.y - 1);

        return IsFit(newPosition);
    }

    public virtual bool IsLeftSpaceFree(Vector2Int position)
    {
        Vector2Int newPosition = new Vector2Int(position.x - 1, position.y);

        return IsFit(newPosition);
    }

    public virtual bool IsRightSpaceFree(Vector2Int position)
    {
        Vector2Int newPosition = new Vector2Int(position.x + 1, position.y);

        return IsFit(newPosition);
    }

    public bool IsFit(Vector2Int position)
    {
        return (position.x < gridSize.x && position.y < gridSize.y && position.x >= 0 && position.y >= 0
                && !grid[position.x, position.y]);
    }
}
