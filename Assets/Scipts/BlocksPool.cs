using System.Collections.Generic;

public class BlocksPool : IPool<Block>
{
    private IFactory<Block> factory = null;

    private Stack<Block> blocks = new Stack<Block>();

    public Block Get()
    {
        if (blocks.Count < 1)
        {
            return factory.Create();
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
    }
}