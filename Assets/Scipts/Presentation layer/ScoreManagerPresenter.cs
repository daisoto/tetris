using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class ScoreManagerPresenter : ConstructableBehaviour<ScoreManager>
{
    [SerializeField] private Text scoreText = null;

    [SerializeField] private Text linesText = null;

    [SerializeField] private string scoreLabel = "Score:";

    [SerializeField] private string linesLabel = "Lines:";

    protected override void Subscribe()
    {
        disposablesContainer.Add(model.score.Subscribe(score =>
        {
            scoreText.DOFade(0, 0.1f).OnComplete(() =>
            {
                scoreText.text = scoreLabel + " " + score.ToString();
                scoreText.DOFade(1, 0.1f);
            });
        }));

        disposablesContainer.Add(model.lineNum.Subscribe(lineNum =>
        {
            linesText.DOFade(0, 0.1f).OnComplete(() =>
            {
                linesText.text = linesLabel + " " + lineNum.ToString();
                linesText.DOFade(1, 0.1f);
            });
        }));
    }
}
