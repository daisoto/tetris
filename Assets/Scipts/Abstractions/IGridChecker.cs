using UnityEngine;

public interface IGridChecker
{
    bool IsNextSpaceFree(Vector2Int position);
    bool IsRowColumnFilled(Vector2Int position, out Vector2Int[] positionsToClear);
}
