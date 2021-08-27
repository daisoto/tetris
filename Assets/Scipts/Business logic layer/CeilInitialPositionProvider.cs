using UnityEngine;

public class CeilInitialPositionProvider : IInitialPositionProvider
{
    public Vector2Int GetInitialPosition(Vector2Int gridSize)
    {
        return new Vector2Int(Misc.GetBankRounded((gridSize.x - 1) / 2f), gridSize.y);
    }
}