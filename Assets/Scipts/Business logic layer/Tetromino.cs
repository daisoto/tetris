using System;
using UniRx;

public class Tetromino : ITickable
{
    public ReactiveProperty<bool> isStuck = new ReactiveProperty<bool>(false);

    public Block[] blocks { get; private set; }

    public bool[,] shape { get; private set; }

    private TetrisGrid tetrisGrid = null;

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    public Tetromino(TetrisGrid tetrisGrid, Block[] blocks, bool[,] shape)
    {
        this.tetrisGrid = tetrisGrid;
        this.blocks = blocks;
        this.shape = shape;

        foreach (Block block in blocks)
        {
            disposablesContainer.Add(block.isStuck.Subscribe(isStuck =>
            {
                if (isStuck)
                {
                    Array.ForEach(blocks, block => block.isStuck.Value = true);
                    this.isStuck.Value = true;
                    disposablesContainer.Clear();
                }
            }));

            disposablesContainer.Add(block.isAlive.Subscribe(isAlive =>
            {
                if (!isAlive)
                {
                    Destroy();
                }
            }));
        }
    }

    public void Destroy()
    {
        foreach (Block block in blocks)
        {
            if (block != null)
            {
                block.isAlive.Value = false;
            }
        }

        Array.Clear(blocks, 0, blocks.Length);
    }

    public void Tick()
    {
        if (!isStuck.Value)
        {
            tetrisGrid.MoveDefault(blocks);
        }
    }

    public void Rotate()
    {
        tetrisGrid.Rotate(blocks);
    }

    public void MoveLeft()
    {
        tetrisGrid.MoveLeft(blocks);
    }

    public void MoveRight()
    {
        tetrisGrid.MoveRight(blocks);
    }

    public void MoveDown()
    {
        tetrisGrid.MoveDown(blocks);
    }
}
