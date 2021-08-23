using System;

public class ScoreEvent : IScoreEvent
{
    public DateTime timeRaised { get; private set; }

    public int nuOfClearedRowsColumns { get; private set; }

    public ScoreEvent(int nuOfClearedRowsColumns)
    {
        this.nuOfClearedRowsColumns = nuOfClearedRowsColumns;
        timeRaised = DateTime.Now;
    }
}