using UnityEngine;

public class CeilInitialPositionProvider : IInitialPositionProvider
{
    private Vector2Int gridSize = default;

    public CeilInitialPositionProvider(Vector2Int gridSize)
    {
        this.gridSize = gridSize;
    }

    public Vector2Int GetInitialPosition(Vector2Int shapeSize)
    {
        int xFreeSpace = gridSize.x - shapeSize.y;

        return new Vector2Int(xFreeSpace / 2, gridSize.y);
    }
}