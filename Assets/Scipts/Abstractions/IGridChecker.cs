using UnityEngine;

public interface IGridChecker
{
    bool IsDefaultSpaceFree(Vector2Int position);
    bool IsDownSpaceFree(Vector2Int position);
    bool IsLeftSpaceFree(Vector2Int position);
    bool IsRightSpaceFree(Vector2Int position);
    bool IsRowColumnFilled(Vector2Int position, out Vector2Int[] positionsToClear);
}
