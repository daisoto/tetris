using UniRx;

public class BlocksFactory : IFactory<Block>
{
    public ReactiveCommand<Block> OnBlockCreate = new ReactiveCommand<Block>();

    private BlockData blockData = null;

    public Block Create()
    {
        Block block = new Block();

        if (blockData != null)
        {
            block = new Block(blockData.color, blockData.sprite);
        }

        OnBlockCreate.Execute(block);

        return block;
    }

    public void SetBlockData(BlockData blockData)
    {
        this.blockData = blockData;
    }
}