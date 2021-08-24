using UniRx;

public class RoundsManager : ConstructableBehaviour<RoundData[]>
{
    public ReactiveCommand OnRoundsFinish = new ReactiveCommand();
    public ReactiveCommand<RoundData> OnRoundSet = new ReactiveCommand<RoundData>();

    private int currentIndex = 0;

    private IFactory<Tetromino> tetrominoFactory = null;

    private Round currentRound = null;

    public void Construct(RoundData[] roundDatas, IFactory<Tetromino> tetrominoFactory)
    {
        this.tetrominoFactory = tetrominoFactory;

        base.Construct(roundDatas);
    }

    public void StartNewRound()
    {
        if (currentIndex > model.Length)
        {
            OnRoundsFinish.Execute();

            return;
        }

        SetNewRound();
        currentRound.StartRound();
    }

    private void SetNewRound()
    {
        currentRound = gameObject.AddComponent<Round>();

        disposablesContainer.Add(currentRound.OnRoundEnd.Subscribe(_ =>
        {
            disposablesContainer.Clear();
            StartNewRound();
        }));

        RoundData currentData = model[currentIndex];

        currentRound.Construct(currentData, tetrominoFactory);
        currentIndex++;

        OnRoundSet.Execute(currentData);
    }
}