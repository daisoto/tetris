using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RoundsManager : ConstructableBehaviour<RoundData[]>
{
    public ReactiveCommand OnRoundsFinish = new ReactiveCommand();

    public ReactiveCommand<BlockData> OnBlockDataSet = new ReactiveCommand<BlockData>();

    public ReactiveProperty<Tetromino> currentTetromino = new ReactiveProperty<Tetromino>();
    public ReactiveProperty<Tetromino> nextTetromino = new ReactiveProperty<Tetromino>();

    public ReactiveProperty<int> roundNumber = new ReactiveProperty<int>();

    private bool isPlaying = false;

    private float currentFallPeriod => currentRound.fallPeriod;

    private Queue<Round> rounds = new Queue<Round>();

    private Dictionary<Round, BlockData> roundsBlockData = new Dictionary<Round, BlockData>();

    private Round currentRound = null;

    private IEnumerator updateRoundCoroutine = null;

    private IFactory<Tetromino> tetrominoFactory = null;

    public void Construct(RoundData[] roundDatas, IFactory<Tetromino> tetrominoFactory)
    {
        model = roundDatas;

        this.tetrominoFactory = tetrominoFactory;

        isConstructed = true;
    }

    public bool TryStart()
    {
        ResetRoundBlocksData();
        ClearTetrominos();

        if (disposablesContainer.size < 1)
        {
            Subscribe();
        }

        roundNumber.Value = 0;
        isPlaying = true;

        return TryStartNewRound();
    }

    public void Stop()
    {
        isPlaying = false;
        currentRound = null;

        Pause();
        Unsubscribe();
    }

    public void Pause()
    {
        if (updateRoundCoroutine != null)
        {
            StopCoroutine(updateRoundCoroutine);
        }
    }

    public void Continue()
    {
        updateRoundCoroutine = UpdateRound();
        StartCoroutine(updateRoundCoroutine);
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

    private void ResetRoundBlocksData()
    {
        rounds.Clear();
        roundsBlockData.Clear();

        foreach (RoundData roundData in model)
        {
            Round round = new Round(roundData, tetrominoFactory);
            rounds.Enqueue(round);
            roundsBlockData.Add(round, roundData.blockData);
        }
    }

    private bool TryStartNewRound()
    {
        Pause();

        if (rounds.Count < 1)
        {
            isPlaying = false;
            OnRoundsFinish.Execute();

            return false;
        }

        if (isPlaying)
        {
            StartNewRound();
        }

        return isPlaying;
    }

    private void StartNewRound()
    {
        SetNewRound();
        currentRound.StartRound();
        roundNumber.Value++;

        Continue();
    }

    private void SetNewRound()
    {
        currentRound = rounds.Dequeue();
        OnBlockDataSet.Execute(roundsBlockData[currentRound]);
        Subscribe();
    }

    private void ClearTetrominos()
    {
        if (currentTetromino.Value != null)
        {
            currentTetromino.Value.Destroy();
            currentTetromino.Value = null;
        }
        if (nextTetromino.Value != null)
        {
            nextTetromino.Value.Destroy();
            nextTetromino.Value = null;
        }
    }

    private IEnumerator UpdateRound()
    {
        while (currentRound != null)
        {
            yield return new WaitForSeconds(currentFallPeriod);

            currentRound.Tick();
        }
    }
}