using UnityEngine;

public class LeftMover : IMover
{
    public Vector2Int GetMovedPosition(Vector2Int position)
    {
        return new Vector2Int(position.x - 1, position.y);
    }
}
