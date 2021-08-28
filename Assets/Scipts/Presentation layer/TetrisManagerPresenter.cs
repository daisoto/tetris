using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TetrisManagerPresenter : ConstructableBehaviour<TetrisManager>
{
    [SerializeField] private string newGameString = "Start new game";

    [SerializeField] private string continueGameString = "Continue";

    [SerializeField] private string exitGameString = "Exit";

    [SerializeField] private string pauseGameString = "Pause";

    [SerializeField] private float fadeDuration = 0.5f;

    [Space]

    [SerializeField] private CanvasGroup menuGroup = null;

    [Space]

    [SerializeField] private Button startButton = null;

    [SerializeField] private Text startText = null;

    [Space]

    [SerializeField] private Button continueButton = null;

    [SerializeField] private Text continueText = null;

    [SerializeField] private GameObject continueDelimeter = null;

    [Space]

    [SerializeField] private CanvasGroup pauseGroup = null;

    [SerializeField] private Button pauseButton = null;

    [SerializeField] private Text pauseText = null;

    [Space]

    [SerializeField] private Button exitButton = null;

    [SerializeField] private Text exitText = null;

    private bool isMenuOpened = true;

    private void Awake()
    {
        startText.text = newGameString;
        continueText.text = continueGameString;
        pauseText.text = pauseGameString;
        exitText.text = exitGameString;

        continueDelimeter.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    protected override void Subscribe()
    {
        disposablesContainer.Add(model.OnGameOver.Subscribe(_ =>
        {
            SetPause(false);
        }));

        disposablesContainer.Add(model.OnRoundsFinish.Subscribe(_ =>
        {
            SetPause(false);
        }));

        disposablesContainer.Add(model.OnPause.Subscribe(_ =>
        {
            SetPause(true);
        }));

        startButton.onClick.AddListener(model.StartGame);
        startButton.onClick.AddListener(CloseMenu);

        continueButton.onClick.AddListener(model.ContinueGame);
        continueButton.onClick.AddListener(CloseMenu);

        pauseButton.onClick.AddListener(model.PauseGame);
        pauseButton.onClick.AddListener(() => SetPause(true));

        exitButton.onClick.AddListener(model.Exit);
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        startButton.onClick.RemoveAllListeners();

        continueButton.onClick.RemoveAllListeners();

        pauseButton.onClick.RemoveAllListeners();
    }

    private void SetPause(bool isContinuePossible)
    {
        continueDelimeter.SetActive(isContinuePossible);
        continueButton.gameObject.SetActive(isContinuePossible);

        OpenMenu();
    }

    private void CloseMenu()
    {
        if (!isMenuOpened)
        {
            return;
        }

        menuGroup.DOFade(0, fadeDuration).OnComplete(() =>
        {
            menuGroup.gameObject.SetActive(false);
        });

        pauseGroup.DOFade(1, fadeDuration).OnComplete(() =>
        {
            pauseButton.gameObject.SetActive(true);
            isMenuOpened = false;
        });
    }

    private void OpenMenu()
    {
        if (isMenuOpened)
        {
            return;
        }

        pauseGroup.DOFade(0, fadeDuration).OnComplete(() =>
        {
            pauseButton.gameObject.SetActive(false);
        });

        menuGroup.gameObject.SetActive(true);
        menuGroup.DOFade(1, fadeDuration).OnComplete(() => { isMenuOpened = true; });
    }
}