using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ScoreManagerPresenter : ConstructableBehaviour<ScoreManager>
{
    [SerializeField] private Text scoreText = null;

    [SerializeField] private Text linesText = null;

    protected override void Subscribe()
    {
        disposablesContainer.Add(model.score.Subscribe(score =>
        {
            scoreText.text = score.ToString();
        }));

        disposablesContainer.Add(model.lineNum.Subscribe(lineNum =>
        {
            linesText.text = lineNum.ToString();
        }));
    }
}
