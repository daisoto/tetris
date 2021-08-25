using UnityEngine;
using UniRx;
using System.Collections;

public class TetrisManager: MonoBehaviour
{
    [SerializeField] private RoundsManager roundsManager = null;

    [SerializeField] private BlockBehavioursGenerator blockBehavioursGenerator = null;

    [SerializeField] private TetrisSettingsData tetrisSettingsData = null;

    private ScoreManager scoreManager = null;

    private bool[,] grid = null;

    private IBlocksMover blocksMover = null;
    private IBlocksRotator blocksRotator = null;
    private IGridChecker gridChecker = null;

    private TetrisGrid tetrisGrid = null;

    private TetrominoFactory tetrominoFactory = null;
    private BlocksFactory blocksFactory = null;
    private BlocksPool blocksPool = null;

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    private DisposablesContainer inputDisposablesContainer = new DisposablesContainer();

    private InputManager inputManager = null;
    private IEnumerator inputCoroutine = null;

    private void Awake()
    {
        Install();
    }

    private void Start()
    {
        inputManager.SetActive(true);

        roundsManager.StartNewRound();
    }

    private void OnEnable()
    {
        disposablesContainer.Add(tetrisGrid.OnBlocksClear.Subscribe(rawScore =>
        {
            scoreManager?.SendScore(rawScore);
        }));

        disposablesContainer.Add(roundsManager.OnRoundSet.Subscribe(roundData =>
        {
            blocksPool?.Clear();
            blocksFactory?.SetBlockData(roundData.blockData);
        }));

        BindInputManager();

        StartCoroutine(inputCoroutine);
    }

    private void OnDisable()
    {
        disposablesContainer.Clear();

        StopCoroutine(inputCoroutine);
    }

    private void Install()
    {
        scoreManager = new ScoreManager();
        grid = new bool[tetrisSettingsData.gridSize.x, tetrisSettingsData.gridSize.y + tetrisSettingsData.GetMaxTetrominoSize().y];

        blocksMover = new DownBlocksMover(grid);
        blocksRotator = new RightAngleBlocksRotator(grid);
        gridChecker = new DownGridChecker(grid);

        tetrisGrid = new TetrisGrid(grid, gridChecker, blocksMover, blocksRotator);

        blocksFactory = new BlocksFactory();
        blocksPool = new BlocksPool(blocksFactory);
        tetrominoFactory = new TetrominoFactory(tetrisSettingsData.initialPosition, tetrisGrid, blocksPool, tetrisSettingsData.tetrominoDatas);

        blockBehavioursGenerator.Construct(blocksFactory, tetrisSettingsData.blockSize);
        roundsManager.Construct(tetrisSettingsData.roundDatas, tetrominoFactory);

        inputManager = new InputManager();

        inputCoroutine = ProcessInput();
    }

    private void BindInputManager()
    {
        disposablesContainer.Add(tetrominoFactory.OnTetrominoCreated.Subscribe(tetromino =>
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

            inputDisposablesContainer.Add(tetromino.isStuck.Subscribe(isStuck =>
            {
                if (isStuck)
                {
                    inputDisposablesContainer.Clear();
                }
            }));
        }));
    }

    private IEnumerator ProcessInput()
    {
        while (true)
        {
            inputManager.Tick();
            yield return new WaitForSeconds(tetrisSettingsData.inputUpdateTime);
        }
    }

    private IEnumerator UpdateTetromino()
    {
        
    }
}
