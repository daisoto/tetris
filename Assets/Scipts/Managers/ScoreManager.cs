using System;
using UniRx;

public class ScoreManager
{
    public ReactiveProperty<int> currentScore = new ReactiveProperty<int>();

    public ReactiveProperty<DateTime> lastScoreTime = new ReactiveProperty<DateTime>();

    private ReactiveCollection<IScoreEvent> scoreEvents = new ReactiveCollection<IScoreEvent>();

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    private Func<int, int> scoreFunc = null;

    public ScoreManager()
    {
        scoreFunc = x => (int)Math.Pow(x, 2);

        disposablesContainer.Add(scoreEvents.ObserveAdd().Subscribe(addEvent =>
        {
            currentScore.Value = scoreFunc(addEvent.Value.nuOfClearedRowsColumns);
            lastScoreTime.Value = addEvent.Value.timeRaised;
        }));
    }

    public ScoreManager(Func<int, int> scoreFunc)
    {
        this.scoreFunc = scoreFunc;

        disposablesContainer.Add(scoreEvents.ObserveAdd().Subscribe(addEvent =>
        {
            currentScore.Value = this.scoreFunc(addEvent.Value.nuOfClearedRowsColumns);
            lastScoreTime.Value = addEvent.Value.timeRaised;
        }));
    }

    public void SendScore(int rawScore)
    {
        ScoreEvent scoreEvent = new ScoreEvent(rawScore); 
        scoreEvents.Add(scoreEvent);
    }

    public void ChangeScoreFunc(Func<int, int> scoreFunc)
    {
        this.scoreFunc = scoreFunc;
    }
}
