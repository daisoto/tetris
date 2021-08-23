using UniRx;
using System.Collections.Generic;

public class BlocksPool : IPool<Block>
{
    private IFactory<Block> factory = null;

    private Stack<Block> blocks = new Stack<Block>();

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    public BlocksPool(IFactory<Block> factory)
    {
        this.factory = factory;
    }

    public Block Get()
    {
        if (blocks.Count < 1)
        {
            Block block = factory.Create();
            block.isAlive.Value = true;

            disposablesContainer.Add(block.isAlive.Subscribe(isAlive =>
            {
                if (!isAlive)
                {
                    Return(block);
                }
            }));

            return block;
        }
        else
        {
            Block block = blocks.Pop();
            block.isAlive.Value = true;
            
            return block;
        }
    }

    public void Return(Block block)
    {
        block.isAlive.Value = false;
        blocks.Push(block);
    }

    public void Clear()
    {
        blocks.Clear();
        disposablesContainer.Clear();
    }
}