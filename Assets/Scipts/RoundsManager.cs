using UniRx;
using UnityEngine;

public class RoundsManager : ConstructableBehaviour<RoundData[]>
{
    public ReactiveCommand OnRoundsFinish = new ReactiveCommand();
    public ReactiveCommand<RoundData> OnRoundSet = new ReactiveCommand<RoundData>();

    [SerializeField] private Round roundPrefab = null;

    private int currentIndex = 0;

    private IFactory<Tetromino> tetrominoFactory = null;

    private Round round = null;

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
        round.StartRound();
    }

    private void SetNewRound()
    {
        round = Instantiate(roundPrefab, transform);

        disposablesContainer.Add(round.OnRoundEnd.Subscribe(_ =>
        {
            disposablesContainer.Clear();
            StartNewRound();
        }));

        RoundData currentData = model[currentIndex];

        round.Construct(currentData, tetrominoFactory);
        currentIndex++;

        OnRoundSet.Execute(currentData);
    }
}