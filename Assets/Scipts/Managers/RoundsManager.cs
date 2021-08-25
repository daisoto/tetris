using System.Collections;
using UniRx;
using UnityEngine;

public class RoundsManager : ConstructableBehaviour<RoundData[]>
{
    public ReactiveCommand OnRoundsFinish = new ReactiveCommand();
    public ReactiveCommand<RoundData> OnRoundSet = new ReactiveCommand<RoundData>();

    private int currentIndex = 0;

    private IFactory<Tetromino> tetrominoFactory = null;

    private float currentFallPeriod = default;

    private Round currentRound = null;

    private IEnumerator updateRoundCoroutine = null;

    public void Construct(RoundData[] roundDatas, IFactory<Tetromino> tetrominoFactory)
    {
        this.tetrominoFactory = tetrominoFactory;
        updateRoundCoroutine = UpdateRound();

        base.Construct(roundDatas);
    }

    public void StartNewRound()
    {
        if (updateRoundCoroutine != null)
        {
            StopCoroutine(updateRoundCoroutine);
        }

        if (currentIndex > model.Length)
        {
            OnRoundsFinish.Execute();

            return;
        }

        SetNewRound();
        currentRound.StartRound();
        StartCoroutine(updateRoundCoroutine);
    }

    private void SetNewRound()
    {
        RoundData currentData = model[currentIndex];

        currentRound = new Round(currentData, tetrominoFactory);

        disposablesContainer.Add(currentRound.OnRoundEnd.Subscribe(_ =>
        {
            disposablesContainer.Clear();
            StartNewRound();
        }));

        currentIndex++;

        currentFallPeriod = currentData.fallPeriod;

        OnRoundSet.Execute(currentData);
    }

    private IEnumerator UpdateRound()
    {
        while (currentRound != null)
        {
            currentRound.Tick();

            yield return new WaitForSeconds(currentFallPeriod);
        }
    }
}