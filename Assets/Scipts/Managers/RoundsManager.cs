using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RoundsManager : ConstructableBehaviour<RoundData[]>
{
    public ReactiveCommand OnRoundsFinish = new ReactiveCommand();
    public ReactiveCommand OnGameOver = new ReactiveCommand(); // TODO : инвокать

    public ReactiveCommand<BlockData> OnBlockDataSet = new ReactiveCommand<BlockData>();

    public ReactiveProperty<Tetromino> currentTetromino = new ReactiveProperty<Tetromino>();
    public ReactiveProperty<Tetromino> nextTetromino = new ReactiveProperty<Tetromino>();

    private float currentFallPeriod => currentRound.fallPeriod;

    private Queue<Round> rounds = new Queue<Round>();

    private Dictionary<Round, BlockData> roundsBlockData = new Dictionary<Round, BlockData>();

    private Round currentRound = null;

    private IEnumerator updateRoundCoroutine = null;

    public void Construct(RoundData[] roundDatas, IFactory<Tetromino> tetrominoFactory)
    {
        foreach (RoundData roundData in roundDatas)
        {
            Round round = new Round(roundData, tetrominoFactory);
            rounds.Enqueue(round);
            roundsBlockData.Add(round, roundData.blockData);
        }

        isConstructed = true;
    }

    protected override void Subscribe()
    {
        disposablesContainer.Add(currentRound?.OnRoundEnd.Subscribe(_ =>
        {
            Unsubscribe();
            TryStartNewRound();
        }));

        disposablesContainer.Add(currentRound?.currentTetromino.Subscribe(tetromino =>
        {
            currentTetromino.Value = tetromino;
        }));

        disposablesContainer.Add(currentRound?.nextTetromino.Subscribe(tetromino =>
        {
            nextTetromino.Value = tetromino;
        }));
    }

    public bool TryStartNewRound()
    {
        if (updateRoundCoroutine != null)
        {
            StopCoroutine(updateRoundCoroutine);
        }

        if (rounds.Count < 1)
        {
            OnRoundsFinish.Execute();

            return false;
        }

        StartNewRound();

        return true;
    }

    private void StartNewRound()
    {
        SetNewRound();
        currentRound.StartRound();
        updateRoundCoroutine = UpdateRound();
        StartCoroutine(updateRoundCoroutine);
    }

    private void SetNewRound()
    {
        currentRound = rounds.Dequeue();
        OnBlockDataSet.Execute(roundsBlockData[currentRound]);
        Subscribe();
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