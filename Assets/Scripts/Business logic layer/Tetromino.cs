using System;
using UniRx;

public class Tetromino : ITickable
{
    public ReactiveProperty<bool> isStuck = new ReactiveProperty<bool>(false);

    public ReactiveCommand OnMoveLeft = new ReactiveCommand();
    public ReactiveCommand OnMoveRight = new ReactiveCommand();
    public ReactiveCommand OnMoveDown = new ReactiveCommand();
    public ReactiveCommand OnRotate = new ReactiveCommand();
    public ReactiveCommand OnTick = new ReactiveCommand();

    public Block[] blocks { get; private set; }

    public bool[,] shape { get; private set; }

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    public Tetromino(Block[] blocks, bool[,] shape)
    {
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
            OnTick.Execute();
        }
    }

    public void Rotate()
    {
        OnRotate.Execute();
    }

    public void MoveLeft()
    {
        OnMoveLeft.Execute();
    }

    public void MoveRight()
    {
        OnMoveRight.Execute();
    }

    public void MoveDown()
    {
        OnMoveDown.Execute();
    }
}
