using System;

public interface IScoreEvent
{        
    DateTime timeRaised { get; }
    int nuOfClearedRowsColumns { get; }
}