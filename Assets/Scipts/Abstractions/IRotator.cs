using UnityEngine;

public interface IRotator
{
    Vector2Int GetRotatedPosition(Vector2Int position, Vector2Int axis);
}
