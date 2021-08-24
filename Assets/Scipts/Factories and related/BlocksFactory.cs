using UniRx;

public class BlocksFactory : IFactory<Block>
{
    public ReactiveCommand<Block> OnBlockCreate = new ReactiveCommand<Block>();

    private BlockData blockData = null;

    public Block Create()
    {
        Block block = null;

        if (blockData != null)
        {
            block = new Block(blockData.color, blockData.sprite);
        }
        else
        {
            block = new Block();
        }

        OnBlockCreate.Execute(block);

        return block;
    }

    public void SetBlockData(BlockData blockData)
    {
        this.blockData = blockData;
    }
}