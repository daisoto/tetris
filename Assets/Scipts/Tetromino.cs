using UniRx;

public class Tetromino: ITickable
{
    public ReactiveProperty<bool> isStuck = new ReactiveProperty<bool>(false);

    public Block[] blocks { get; private set; }

    private TetrisGrid tetrisGrid = null;

    public Tetromino(TetrisGrid tetrisGrid, Block[] blocks)
    {
        this.tetrisGrid = tetrisGrid;
        this.blocks = blocks;

        foreach (Block block in blocks)
        {
            block.isStuck.Subscribe(isStuck =>
            {
                if (isStuck)
                {
                    this.isStuck.Value = true;
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
