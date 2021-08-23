using UniRx;

public class RoundsManager
{
    public ReactiveCommand OnGameFinish = new ReactiveCommand();

    private RoundData[] roundDatas = null;

    private int currentIndex = -1;

    private Round round = null;

    private IPool<Block> blocksPool = null;

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    public RoundsManager(RoundData[] roundDatas, Round round, IPool<Block> blocksPool)
    {
        this.blocksPool = blocksPool;
        this.roundDatas = roundDatas;
        this.round = round;
    }

    public void StartNewRound()
    {
        blocksPool.Clear();

        if (currentIndex + 1 > roundDatas.Length)
        {
            OnGameFinish.Execute();

            return;
        }

        disposablesContainer.Add(round.OnRoundEnd.Subscribe(_ => 
        {
            disposablesContainer.Clear();
            StartNewRound();
        }));

        currentIndex++;
        round.Construct(roundDatas[currentIndex]);
        round.StartRound();
    }
}