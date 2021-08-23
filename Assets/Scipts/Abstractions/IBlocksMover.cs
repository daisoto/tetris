public interface IBlocksMover
{
    void MoveDefault(Block[] blocks);
    void MoveDefault(Block block);
    void MoveRight(Block[] blocks);
    void MoveLeft(Block[] blocks);
    void MoveDown(Block[] blocks);
}
