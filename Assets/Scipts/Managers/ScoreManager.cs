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
        scoreFunc = x => // default tetris scoring
        {
            if (x < 1)
            {
                return 0;
            }
            if (x == 1)
            {
                return 100;
            }
            if (x == 2)
            {
                return 300;
            }
            if (x == 3)
            {
                return 700;
            }
            if (x == 4)
            {
                return 1500;
            }

            return 2000;
        };

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
