using UnityEngine;

public class DownGridChecker : IGridChecker
{
    public bool IsNextSpaceFree(Vector2Int position, bool[,] cells)
    {
        return cells[position.x, position.y + 1];
    }

    public bool IsRowColumnFilled(Vector2Int position, bool[,] cells, out Vector2Int[] positionsToClear)
    {
        positionsToClear = new Vector2Int[position.x];

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            if (cells[position.x, i] == false)
            {
                positionsToClear = null;

                return false;
            }

            positionsToClear[i] = new Vector2Int(position.x, i);
        }

        return true;
    }
}
