using UnityEngine;

public interface IGridChecker
{
    bool IsDefaultSpaceFree(Vector2Int position);
    bool IsDownSpaceFree(Vector2Int position);
    bool IsLeftSpaceFree(Vector2Int position);
    bool IsRightSpaceFree(Vector2Int position);

    int GetScoreFromFilled(Vector2Int[] insertedPositions, out Vector2Int[] positionsToClear);

    bool IsFit(Vector2Int position);
}
