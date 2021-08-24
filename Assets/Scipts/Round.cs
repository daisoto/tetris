using UniRx;
using System.Collections;
using UnityEngine;

public class Round : ConstructableBehaviour<RoundData>
{
    public ReactiveCommand OnRoundEnd = new ReactiveCommand();

    private RoundData roundData = null;

    private IFactory<Tetromino> tetrominoFactory = null;

    private Tetromino currentTetromino = null;

    private int tetrominoCounter = 0;

    private IEnumerator coroutine = null;

    public void Construct(RoundData roundData, IFactory<Tetromino> tetrominoFactory)
    {
        this.tetrominoFactory = tetrominoFactory;
        coroutine = UpdateTick();

        Construct(roundData);
    }

    public void StartRound()
    {
        tetrominoCounter = 0;
        ContinueRound();
    }

    public void PauseRound()
    {
        StopCoroutine(coroutine);
    }

    public void ContinueRound()
    {
        if (tetrominoCounter > roundData.tetrominosInRound)
        {
            StopCoroutine(coroutine);

            OnRoundEnd.Execute();

            return;
        }

        if (currentTetromino == null)
        {
            disposablesContainer.Clear();
            SetCurrentTetramino();
        }

        StartCoroutine(coroutine);
    }

    private void SetCurrentTetramino()
    {
        currentTetromino = tetrominoFactory?.Create();

        disposablesContainer.Add(currentTetromino.OnStuck.Subscribe(_ =>
        {
            currentTetromino = null;
            tetrominoCounter++;
            ContinueRound();
        }));
    }

    private IEnumerator UpdateTick()
    {
        yield return new WaitForSeconds(roundData.fallPeriod);
        currentTetromino?.Tick();
    }
}