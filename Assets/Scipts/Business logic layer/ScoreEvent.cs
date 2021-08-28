public class ScoreEvent : IScoreEvent
{
    public int rawScore { get; private set; }

    public ScoreEvent(int rawScore)
    {
        this.rawScore = rawScore;
    }
}