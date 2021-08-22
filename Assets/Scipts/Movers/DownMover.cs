using UnityEngine;

public class DownMover : IMover
{
    public Vector2Int GetMovedPosition(Vector2Int position)
    {
        return new Vector2Int(position.x, position.y + 1);
    }
}
