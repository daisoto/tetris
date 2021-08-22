public class BlockFactory : IFactory<Block>
{
    private BlockData blockData = null;

    public Block Create()
    {
        if (blockData == null)
        {
            return new Block();
        }
        else
        {
            return new Block(blockData.color, blockData.sprite);
        }
    }

    public void SetBlockData(BlockData blockData)
    {
        this.blockData = blockData;
    }
}