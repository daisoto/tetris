using UnityEngine;
using UniRx;

public class TetrisManager: MonoBehaviour
{
    [SerializeField] private RoundsManager roundsManager = null;

    [SerializeField] private BlockBehavioursGenerator blockBehavioursGenerator = null;

    [SerializeField] private TetrisSettingsData tetrisSettingsData = null;

    [SerializeField] private InputManager inputManager = null;

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

    private void Awake()
    {
        Install();
    }

    private void Start()
    {
        inputManager.SetActive(true);
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
    }

    private void OnDisable()
    {
        disposablesContainer.Clear();
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
    }
}
