using UnityEngine;
using UniRx;

public class TetrisManager: MonoBehaviour
{
    [SerializeField] private TetrisSettingsData tetrisSettingsData = null;

    [Space(20)]

    [SerializeField] private RoundsManager roundsManager = null;

    [SerializeField] private BlockBehavioursGenerator blockBehavioursGenerator = null;

    [SerializeField] private InputManager inputManager = null;

    [SerializeField] private TetrominoBindingManager tetrominoBindingManager = null;

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

    private void Awake()
    {
        Install();
    }

    [ContextMenu("Start")]
    public void StartGame()
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
            blocksPool?.Clear();
        }));

        disposablesContainer.Add(roundsManager.OnRoundsFinish.Subscribe(_ =>
        {
            scoreManager?.Reset();
            blocksPool?.Clear();
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

        tetrisGrid = new TetrisGrid(grid);

        blocksFactory = new BlocksFactory(tetrisSettingsData.roundDatas[0].blockData);
        blocksPool = new BlocksPool(blocksFactory);
        tetrominoFactory = new TetrominoFactory(tetrisSettingsData.gridSize, blocksPool, tetrisSettingsData.tetrominoDatas);

        blockBehavioursGenerator.Construct(tetrisSettingsData.gridSize);

        roundsManager.Construct(tetrisSettingsData.roundDatas, tetrominoFactory);

        inputManager.Construct(tetrisSettingsData.inputUpdateTime);

        scoreManagerPresenter.Construct(scoreManager);

        roundsManagerPresenter.Construct(roundsManager);

        tetrominoBindingManager.Construct(tetrisGrid);
    }    
}
