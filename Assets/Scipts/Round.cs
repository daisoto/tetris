using UniRx;
using System.Collections;
using UnityEngine;

public class Round: ConstructableBehaviour<RoundData>
{
    public ReactiveCommand OnRoundEnd = new ReactiveCommand();

    private RoundData roundData = null;
    
    private IFactory<Tetromino> tetrominoFactory = null;
    
    private BlocksFactory blocksFactory = null;

    private Tetromino currentTetromino = null;

    private int tetrominoCounter = 0;

    private IEnumerator coroutine = null;

    public override void Construct(RoundData roundData)
    {
        base.Construct(roundData);

        blocksFactory.SetBlockData(roundData.blockData);

        coroutine = UpdateTick();
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
        StartCoroutine(coroutine);

        if (tetrominoCounter > roundData.tetrominosInRound)
        {
            OnRoundEnd.Execute();
            StopCoroutine(coroutine);

            return;
        }

        currentTetromino = currentTetromino == null ? tetrominoFactory.Create(): currentTetromino;

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