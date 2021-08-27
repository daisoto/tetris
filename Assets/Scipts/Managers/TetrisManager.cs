using UnityEngine;
using UniRx;

public class TetrisManager: MonoBehaviour
{
    [SerializeField] private TetrisSettingsData tetrisSettingsData = null;

    [Space(20)]

    [SerializeField] private RoundsManager roundsManager = null;

    [SerializeField] private BlockBehavioursGenerator blockBehavioursGenerator = null;

    [SerializeField] private InputManager inputManager = null;

    [Space(20)]

    [SerializeField] private ScoreManagerPresenter scoreManagerPresenter = null;

    [SerializeField] private RoundsManagerPresenter roundsManagerPresenter = null;

    private ScoreManager scoreManager = null;

    private bool[,] grid = null;

    private TetrisGrid tetrisGrid = null;

    private TetrominoFactory tetrominoFactory = null;
    private BlocksFactory blocksFactory = null;
    private BlocksPool blocksPool = null;

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    private DisposablesContainer inputDisposablesContainer = new DisposablesContainer();

    private DisposablesContainer tetrominoGridDisposablesContainer = new DisposablesContainer();

    private void Awake()
    {
        Install();
    }

    private void Start()
    {
        inputManager.isActive.Value = true;
        tetrisGrid.ClearGrid();
        roundsManager.TryStart();
    }

    private void OnEnable()
    {      
        disposablesContainer.Add(tetrisGrid.OnBlocksClear.Subscribe(rawScore =>
        {
            scoreManager?.SendScore(rawScore);
        }));

        disposablesContainer.Add(roundsManager.OnBlockDataSet.Subscribe(blockData =>
        {
            blocksPool?.Clear();
            blocksFactory?.SetBlockData(blockData);
        }));

        disposablesContainer.Add(tetrisGrid.onGameOver.Subscribe(_ =>
        {
            scoreManager?.Reset();
            roundsManager?.Stop();
            blocksPool?.Clear();
            inputDisposablesContainer?.Clear();
        }));

        disposablesContainer.Add(roundsManager.OnRoundsFinish.Subscribe(_ =>
        {
            scoreManager?.Reset();
            roundsManager?.Stop();
            blocksPool?.Clear();
            inputDisposablesContainer?.Clear();
        }));

        BindInputManager();
        BindTetrominoActions();
    }

    private void OnDisable()
    {
        disposablesContainer.Clear();
    }

    private void Install()
    {
        scoreManager = new ScoreManager();
        grid = new bool[tetrisSettingsData.gridSize.x, tetrisSettingsData.gridSize.y + tetrisSettingsData.GetMaxTetrominoSize().y];

        tetrisGrid = new TetrisGrid(grid);

        blocksFactory = new BlocksFactory(tetrisSettingsData.roundDatas[0].blockData);
        blocksPool = new BlocksPool(blocksFactory);
        tetrominoFactory = new TetrominoFactory(tetrisSettingsData.gridSize, blocksPool, tetrisSettingsData.tetrominoDatas);

        blockBehavioursGenerator.Construct(tetrisSettingsData.gridSize);

        roundsManager.Construct(tetrisSettingsData.roundDatas, tetrominoFactory);

        inputManager.Construct(tetrisSettingsData.inputUpdateTime);

        scoreManagerPresenter.Construct(scoreManager);

        roundsManagerPresenter.Construct(roundsManager);
    }

    private void BindTetrominoActions()
    {
        disposablesContainer.Add(roundsManager.currentTetromino.Subscribe(tetromino =>
        {
            tetrominoGridDisposablesContainer.Clear();

            tetrominoGridDisposablesContainer.Add(tetromino?.onTick.Subscribe(_ =>
            {
                tetrisGrid?.MoveDefault(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.onMoveLeft.Subscribe(_ =>
            {
                tetrisGrid?.MoveLeft(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.onMoveRight.Subscribe(_ =>
            {
                tetrisGrid?.MoveRight(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.onMoveDown.Subscribe(_ =>
            {
                tetrisGrid?.MoveDown(tetromino);
            }));

            tetrominoGridDisposablesContainer.Add(tetromino?.onRotate.Subscribe(_ =>
            {
                tetrisGrid?.Rotate(tetromino);
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
