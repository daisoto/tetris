using UnityEngine;

public interface IBlocksMover
{
    Vector2Int moveVector { get; }

    void MoveDefault(Block[] blocks);
    void MoveDefault(Block block);

    void MoveRight(Block[] blocks);
    void MoveRight(Block block);

    void MoveLeft(Block[] blocks);
    void MoveLeft(Block block);

    void MoveDown(Block[] blocks);
    void MoveDown(Block block);
}
