using UniRx;

public class Tetromino: ITickable
{
    public ReactiveCommand OnStuck = new ReactiveCommand();

    private Block[] blocks = null;

    private TetrisGrid tetrisGrid = null;

    public Tetromino(Block[] blocks, TetrisGrid tetrisGrid)
    {
        this.tetrisGrid = tetrisGrid;
        this.blocks = blocks;

        foreach (Block block in blocks)
        {
            block.isStuck.Subscribe(isStuck =>
            {
                if (isStuck)
                {
                    OnStuck.Execute();
                }
            });
        }
    }

    public void Tick()
    {
        tetrisGrid.DefaultMove(blocks);
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
