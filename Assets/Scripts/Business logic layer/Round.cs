using UniRx;

public class Round : ITickable
{
	public ReactiveCommand OnRoundEnd = new ReactiveCommand();

	public float fallPeriod { get; private set; }

	public ReactiveProperty<Tetromino> currentTetromino = new ReactiveProperty<Tetromino>();

	public ReactiveProperty<Tetromino> nextTetromino = new ReactiveProperty<Tetromino>();

	private IFactory<Tetromino> tetrominoFactory = null;

	private int currentTetrominoNum = 1;

	private int tetrominosInRound = 0;

	private bool isLastTetromino { get { return currentTetrominoNum == tetrominosInRound; } }

	private bool isFirstTetromino { get { return currentTetrominoNum == 1; } }

	private DisposablesContainer disposablesContainer = new DisposablesContainer();

	public Round(RoundData roundData, IFactory<Tetromino> tetrominoFactory)
	{
		tetrominosInRound = roundData.tetrominosInRound;
		fallPeriod = roundData.fallPeriod;
		this.tetrominoFactory = tetrominoFactory;
	}

	public void Tick()
	{
		currentTetromino.Value?.Tick();
	}

	public void StartRound()
	{
		currentTetrominoNum = 1;

		SetTetraminos();
		SubscribeOnStuck();
	}

	public void ContinueRound()
	{
		if (currentTetrominoNum > tetrominosInRound)
		{
			OnRoundEnd.Execute();

			return;
		}

		disposablesContainer.Clear();

		SetTetraminos();

		SubscribeOnStuck();
	}

	private void SetTetraminos()
	{
		currentTetromino.Value = isFirstTetromino ? tetrominoFactory?.Create() : nextTetromino.Value;
		nextTetromino.Value = isLastTetromino ? null : tetrominoFactory?.Create();		
	}

	private void SubscribeOnStuck()
    {
		disposablesContainer.Add(currentTetromino.Value.isStuck.Subscribe(isStuck =>
		{
			if (isStuck)
			{
				currentTetrominoNum++;
				ContinueRound();
			}
		}));
	}
}