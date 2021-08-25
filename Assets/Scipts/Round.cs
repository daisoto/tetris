using UniRx;

public class Round: ITickable
{
    public ReactiveCommand OnRoundEnd = new ReactiveCommand();

    private RoundData roundData = null;

    private IFactory<Tetromino> tetrominoFactory = null;

    private Tetromino currentTetromino = null;

    private int tetrominoCounter = 0;

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    public Round(RoundData roundData, IFactory<Tetromino> tetrominoFactory)
    {
        this.roundData = roundData;
        this.tetrominoFactory = tetrominoFactory;
    }

    public void Tick()
    {
        currentTetromino?.Tick();
    }

    public void StartRound()
    {
        tetrominoCounter = 0;
        ContinueRound();
    }

    public void ContinueRound()
    {
        if (tetrominoCounter > roundData.tetrominosInRound)
        {
            OnRoundEnd.Execute();

            return;
        }

        if (currentTetromino == null)
        {
            disposablesContainer.Clear();
            SetCurrentTetramino();
        }
    }

    private void SetCurrentTetramino()
    {
        currentTetromino = tetrominoFactory?.Create();

        disposablesContainer.Add(currentTetromino.isStuck.Subscribe(isStuck =>
        {
            if (isStuck)
            {
                currentTetromino = null;
                tetrominoCounter++;
                ContinueRound();
            }
        }));
    }
}