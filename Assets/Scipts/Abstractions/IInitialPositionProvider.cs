using UnityEngine;

public interface IInitialPositionProvider
{
    Vector2Int GetInitialPosition(Vector2Int shapeSize);
}