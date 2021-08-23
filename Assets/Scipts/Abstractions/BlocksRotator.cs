public abstract class BlocksRotator: IBlocksRotator
{
    protected bool[,] grid = null;

    public BlocksRotator(bool[,] grid)
    {
        this.grid = grid;
    }

    public abstract void Rotate(Block[] blocks);
}