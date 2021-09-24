using UniRx;
using UnityEngine;

public class TetrominoBindingManager : ConstructableBehaviour<TetrisGrid>
{
    [SerializeField] private RoundsManager roundsManager = null;

    [SerializeField] private InputManagerBehaviour inputManager = null;

    private DisposablesContainer inputDisposablesContainer = new DisposablesContainer();

    private DisposablesContainer tetrominoGridDisposablesContainer = new DisposablesContainer();

    protected override void Subscribe()
    {
        disposablesContainer.Add(roundsManager.OnRoundsFinish.Subscribe(_ =>
        {
            inputDisposablesContainer?.Clear();
            roundsManager?.Stop();
        })); 
        
        disposablesContainer.Add(model.OnGameOver.Subscribe(_ =>
        {
            roundsManager?.Stop();
            inputDisposablesContainer?.Clear();
        }));

        BindInputManager();
        BindTetrominoActions();

        base.Subscribe();
    }

    private void BindTetrominoActions()
    {
        disposablesContainer.Add(roundsManager.currentTetromino.Subscribe(tetromino =>
        {
            tetrominoGridDisposablesContainer.Clear();

            tetrominoGridDisposablesContainer.Add(tetromino?.OnTick.Subscribe(_ =>
            {
                model?.MoveDefault(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.OnMoveLeft.Subscribe(_ =>
            {
                model?.MoveLeft(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.OnMoveRight.Subscribe(_ =>
            {
                model?.MoveRight(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.OnMoveDown.Subscribe(_ =>
            {
                model?.MoveDown(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.OnRotate.Subscribe(_ =>
            {
                model?.Rotate(tetromino);
            }));
        }));
    }

    private void BindInputManager()
    {
        disposablesContainer.Add(roundsManager.currentTetromino.Subscribe(tetromino =>
        {
            inputDisposablesContainer.Clear();

            inputDisposablesContainer.Add(inputManager.OnDownPress.Subscribe(_ =>
            {
                tetromino?.MoveDown();
            }));

            inputDisposablesContainer.Add(inputManager.OnLeftPress.Subscribe(_ =>
            {
                tetromino?.MoveLeft();
            }));

            inputDisposablesContainer.Add(inputManager.OnRightPress.Subscribe(_ =>
            {
                tetromino?.MoveRight();
            }));

            inputDisposablesContainer.Add(inputManager.OnRotatePress.Subscribe(_ =>
            {
                tetromino?.Rotate();
            }));
        }));
    }
}