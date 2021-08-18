using UnityEngine;

public interface IGridChecker
{
    bool IsNextSpaceFree(Vector2Int position, bool[,] cells);
    bool IsRowColumnFilled(Vector2Int position, bool[,] cells, out Vector2Int[] positionsToClear);
}
