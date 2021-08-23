public class TetrisManager
{
    private ScoreManager scoreManager = new ScoreManager();

    private TetrisSettingsData tetrisSettings = null;

    private bool[,] grid = null;

    private IBlocksMover blocksMover = null;
    private IBlocksRotator blocksRotator = null;
    private IGridChecker gridChecker = null;

    private TetrisGrid tetrisGrid = null;

    private TetrominoFactory tetrominoFactory = null;

    private BlocksFactory blocksFactory = null;
    private BlocksPool blocksPool = null;

    private BlockBehavioursGenerator blockBehavioursGenerator = null;

    private RoundsManager roundsManager = null;
    private Round round = null;

    public TetrisManager(TetrisSettingsData tetrisSettings, Round round, BlockBehavioursGenerator blockBehavioursGenerator)
    {
        this.tetrisSettings = tetrisSettings;

        grid = new bool[tetrisSettings.size.x + 1, tetrisSettings.size.y];

        blocksMover = new DownBlocksMover(grid);
        blocksRotator = new RightAngleBlocksRotator(grid);
        gridChecker = new DownGridChecker(grid);

        tetrisGrid = new TetrisGrid(grid, gridChecker, blocksMover, blocksRotator, scoreManager);

        blocksFactory = new BlocksFactory();
        blocksPool = new BlocksPool(blocksFactory);
        tetrominoFactory = new TetrominoFactory(tetrisSettings.initialPosition, tetrisGrid, blocksPool, tetrisSettings.tetrominoDatas);

        this.blockBehavioursGenerator = blockBehavioursGenerator;

        blockBehavioursGenerator.Construct(blocksFactory);

        this.round = round;
        roundsManager = new RoundsManager(tetrisSettings.roundDatas, round, blocksPool);
    }
}
