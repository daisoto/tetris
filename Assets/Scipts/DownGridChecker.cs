using UnityEngine;

public class DownGridChecker : GridChecker
{
    public DownGridChecker(bool[,] grid) : base(grid) { }

    public override bool IsDefaultSpaceFree(Vector2Int position)
    {
        return IsDownSpaceFree(position);
    }

    public override bool IsRowColumnFilled(Vector2Int position, out Vector2Int[] positionsToClear)
    {
        positionsToClear = new Vector2Int[position.x];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            if (grid[position.x, i] == false)
            {
                positionsToClear = null;

                return false;
            }

            positionsToClear[i] = new Vector2Int(position.x, i);
        }

        return true;
    }
}
