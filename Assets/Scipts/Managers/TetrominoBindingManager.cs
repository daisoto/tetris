using UniRx;
using UnityEngine;

public class TetrominoBindingManager : ConstructableBehaviour<TetrisGrid>
{
    [SerializeField] private RoundsManager roundsManager = null;

    [SerializeField] private InputManager inputManager = null;

    private DisposablesContainer inputDisposablesContainer = new DisposablesContainer();

    private DisposablesContainer tetrominoGridDisposablesContainer = new DisposablesContainer();

    protected override void Subscribe()
    {
        disposablesContainer.Add(roundsManager.OnRoundsFinish.Subscribe(_ =>
        {
            inputDisposablesContainer?.Clear();
            roundsManager?.Stop();
        })); 
        
        disposablesContainer.Add(model.onGameOver.Subscribe(_ =>
        {
            roundsManager?.Stop();
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

            tetrominoGridDisposablesContainer.Add(tetromino?.onTick.Subscribe(_ =>
            {
                model?.MoveDefault(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.onMoveLeft.Subscribe(_ =>
            {
                model?.MoveLeft(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.onMoveRight.Subscribe(_ =>
            {
                model?.MoveRight(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.onMoveDown.Subscribe(_ =>
            {
                model?.MoveDown(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.onRotate.Subscribe(_ =>
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

            inputDisposablesContainer.Add(inputManager.onDownPress.Subscribe(_ =>
            {
                tetromino?.MoveDown();
            }));

            inputDisposablesContainer.Add(inputManager.onLeftPress.Subscribe(_ =>
            {
                tetromino?.MoveLeft();
            }));

            inputDisposablesContainer.Add(inputManager.onRightPress.Subscribe(_ =>
            {
                tetromino?.MoveRight();
            }));

            inputDisposablesContainer.Add(inputManager.onRotatePress.Subscribe(_ =>
            {
                tetromino?.Rotate();
            }));
        }));
    }
}