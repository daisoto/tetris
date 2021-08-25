using UniRx;
using System.Collections;
using UnityEngine;

public class Round : ConstructableBehaviour<RoundData>
{
    public ReactiveCommand OnRoundEnd = new ReactiveCommand();

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
        if (tetrominoCounter > model.tetrominosInRound)
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

    private IEnumerator UpdateTick()
    {
        while (true)
        {
            currentTetromino?.Tick();
            yield return new WaitForSeconds(model.fallPeriod);
        }
    }
}