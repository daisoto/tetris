public class BlocksFactory : IFactory<Block>
{
    private BlockData blockData = null;

    public BlocksFactory(BlockData blockData)
    {
        this.blockData = blockData;
    }

    public Block Create()
    {
        Block block;

        if (blockData != null)
        {
            block = new Block(blockData.color, blockData.sprite);
        }
        else
        {
            block = new Block();
        }

        return block;
    }

    public void SetBlockData(BlockData blockData)
    {
        this.blockData = blockData;
    }
}