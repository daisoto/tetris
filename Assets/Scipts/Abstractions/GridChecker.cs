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

    public abstract bool IsRowColumnFilled(Vector2Int position, out Vector2Int[] positionsToClear);

    public virtual bool IsDownSpaceFree(Vector2Int position)
    {
        Vector2Int newPosition = new Vector2Int(position.x, position.y - 1);

        if (!IsFit(newPosition))
        {
            return false;
        }

        return !grid[newPosition.x, newPosition.y];
    }

    public virtual bool IsLeftSpaceFree(Vector2Int position)
    {
        Vector2Int newPosition = new Vector2Int(position.x - 1, position.y);

        if (!IsFit(newPosition))
        {
            return false;
        }

        return !grid[newPosition.x, newPosition.y];
    }

    public virtual bool IsRightSpaceFree(Vector2Int position)
    {
        Vector2Int newPosition = new Vector2Int(position.x + 1, position.y);

        if (!IsFit(newPosition))
        {
            return false;
        }

        return !grid[newPosition.x, newPosition.y];
    }

    private bool IsFit(Vector2Int position)
    {
        return (position.x < gridSize.x && position.y < gridSize.y && position.x > 0 && position.y > 0);
    }
}
