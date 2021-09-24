using UnityEngine;
using UniRx;

public class TetrisManager: MonoBehaviour
{
    public ReactiveCommand OnGameOver = new ReactiveCommand();

    public ReactiveCommand OnRoundsFinish = new ReactiveCommand();

    public ReactiveCommand OnPause = new ReactiveCommand();

    [SerializeField] private TetrisSettingsData tetrisSettingsData = null;

    [Space]

    [SerializeField] private RoundsManager roundsManager = null;

    [SerializeField] private BlockBehavioursGenerator blockBehavioursGenerator = null;

    [SerializeField] private InputManagerBehaviour inputManagerBehavior = null;

    [SerializeField] private TetrominoBindingManager tetrominoBindingManager = null;

    [Space]

    [SerializeField] private ScoreManagerPresenter scoreManagerPresenter = null;

    [SerializeField] private RoundsManagerPresenter roundsManagerPresenter = null;

    [SerializeField] private TetrisManagerPresenter tetrisManagerPresenter = null;

    private ScoreManager scoreManager = null;

    private InputManager inputManager = null;

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

    public void StartGame()
    {
        blocksPool.Clear();
        scoreManager.Reset();
        inputManager.isActive.Value = true;
        tetrisGrid.ClearGrid();
        roundsManager.TryStart();
    }

    public void StopGame()
    {
        inputManager.isActive.Value = false;
        roundsManager.Stop();
    }

    public void PauseGame()
    {
        inputManager.isActive.Value = false;
        roundsManager.Pause();
    }

    public void ContinueGame()
    {
        inputManager.isActive.Value = true;
        roundsManager.Continue();
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        disposablesContainer.Add(inputManagerBehavior.OnPausePress.Subscribe(_ =>
        {
            PauseGame();
            OnPause.Execute();
        }));

        disposablesContainer.Add(tetrisGrid.OnGameOver.Subscribe(_ =>
        {
            StopGame();
            OnGameOver.Execute();
        }));

        disposablesContainer.Add(roundsManager.OnRoundsFinish.Subscribe(_ =>
        {
            StopGame();
            OnRoundsFinish.Execute();
        }));

        disposablesContainer.Add(tetrisGrid.OnBlocksClear.Subscribe(rawScore =>
        {
            scoreManager?.SendScore(rawScore);
        }));

        disposablesContainer.Add(roundsManager.OnBlockDataSet.Subscribe(blockData =>
        {
            blocksPool?.Clear();
            blocksFactory?.SetBlockData(blockData);
        }));
    }

    private void OnDisable()
    {
        disposablesContainer.Clear();
    }

    private void Install()
    {
        scoreManager = new ScoreManager();
        scoreManagerPresenter.Construct(scoreManager);

        grid = new bool[tetrisSettingsData.gridSize.x, tetrisSettingsData.gridSize.y + tetrisSettingsData.GetMaxTetrominoSize().y];
        tetrisGrid = new TetrisGrid(grid);
        tetrominoBindingManager.Construct(tetrisGrid);

        blocksFactory = new BlocksFactory(tetrisSettingsData.roundDatas[0].blockData);
        blocksPool = new BlocksPool(blocksFactory);
        tetrominoFactory = new TetrominoFactory(tetrisSettingsData.gridSize, blocksPool, tetrisSettingsData.tetrominoDatas);

        blockBehavioursGenerator.Construct(tetrisSettingsData.gridSize);

        roundsManager.Construct(tetrisSettingsData.roundDatas, tetrominoFactory);
        roundsManagerPresenter.Construct(roundsManager);

        inputManager = new InputManager(tetrisSettingsData.inputUpdateTime);
        inputManagerBehavior.Construct(inputManager);

        tetrisManagerPresenter.Construct(this);
    }    
}
