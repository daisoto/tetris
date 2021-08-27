using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class ScoreManagerPresenter : ConstructableBehaviour<ScoreManager>
{
    [SerializeField] private string scoreLabel = "Score:";

    [SerializeField] private string linesLabel = "Lines:";

    [SerializeField] private float fadeDuration = 0.1f;

    [Space]

    [SerializeField] private Text scoreText = null;

    [SerializeField] private Text linesText = null;

    protected override void Subscribe()
    {
        disposablesContainer.Add(model.score.Subscribe(score =>
        {
            scoreText.DOFade(0, fadeDuration).OnComplete(() =>
            {
                scoreText.text = scoreLabel + " " + score.ToString();
                scoreText.DOFade(1, fadeDuration);
            });
        }));

        disposablesContainer.Add(model.lineNum.Subscribe(lineNum =>
        {
            linesText.DOFade(0, fadeDuration).OnComplete(() =>
            {
                linesText.text = linesLabel + " " + lineNum.ToString();
                linesText.DOFade(1, fadeDuration);
            });
        }));
    }
}
