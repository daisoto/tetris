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

    private void Awake()
    {
        Install();
    }

    private void Start()
    {
        inputManager.isActive.Value = true;

        roundsManager.TryStartNewRound();
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
            roundsManager.StopRound();
            blocksPool.Clear();
        }));

        BindInputManager();
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
        tetrominoFactory = new TetrominoFactory(tetrisSettingsData.initialPosition, tetrisGrid, blocksPool, tetrisSettingsData.tetrominoDatas);

        blockBehavioursGenerator.Construct(tetrisSettingsData.gridSize);

        roundsManager.Construct(tetrisSettingsData.roundDatas, tetrominoFactory);

        inputManager.Construct(tetrisSettingsData.inputUpdateTime);
    }

    private void BindInputManager()
    {
        disposablesContainer.Add(roundsManager.currentTetromino.Subscribe(tetromino =>
        {
            inputDisposablesContainer.Add(inputManager.onDownPressed.Subscribe(_ =>
            {
                tetromino?.MoveDown();
            }));

            inputDisposablesContainer.Add(inputManager.onLeftPressed.Subscribe(_ =>
            {
                tetromino?.MoveLeft();
            }));

            inputDisposablesContainer.Add(inputManager.onRightPressed.Subscribe(_ =>
            {
                tetromino?.MoveRight();
            }));

            inputDisposablesContainer.Add(inputManager.onRotatePressed.Subscribe(_ =>
            {
                tetromino?.Rotate();
            }));

            inputDisposablesContainer.Add(tetromino?.isStuck.Subscribe(isStuck =>
            {
                if (isStuck)
                {
                    inputDisposablesContainer.Clear();
                }
            }));
        }));
    }
}
