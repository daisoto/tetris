using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ScoreManagerPresenter : ConstructableBehaviour<ScoreManager>
{
    [SerializeField] private Text scoreText;

    protected override void Subscribe()
    {
        disposablesContainer.Add(model.currentScore.Subscribe(score =>
        {
            scoreText.text = score.ToString();
        }));
    }
}
