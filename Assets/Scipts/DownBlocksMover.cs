public class DownBlocksMover : BlocksMover
{
    public DownBlocksMover(bool[,] grid) : base(grid) { }

    public override void MoveDefault(Block[] blocks)
    {
        MoveDown(blocks);
    }

    public override void MoveDefault(Block block)
    {
        MoveLeft(block);
    }
}
