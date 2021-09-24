public abstract class BlocksRotator: IBlocksRotator
{
    protected IGridChecker gridChecker = null;

    public BlocksRotator(IGridChecker gridChecker)
    {
        this.gridChecker = gridChecker;
    }

    public abstract void Rotate(Block[] blocks);
}