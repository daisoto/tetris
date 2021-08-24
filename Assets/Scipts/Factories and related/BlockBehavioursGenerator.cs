using UniRx;
using UnityEngine;

public class BlockBehavioursGenerator: ConstructableBehaviour<BlocksFactory>
{
    [SerializeField] private BlockBehaviour blockBehaviourPrefab = null;

    private Vector2Int blockSize = Vector2Int.one;

    public void Construct(BlocksFactory blocksFactory, Vector2Int blockSize)
    {
        this.blockSize = blockSize;

        Construct(blocksFactory);
    }

    protected override void OnEnable()
    {
        disposablesContainer.Add(model.OnBlockCreate.Subscribe(block =>
        {
            BlockBehaviour blockBehaviour = Instantiate(blockBehaviourPrefab, transform);
            blockBehaviour.transform.localScale = new Vector3(blockSize.x, blockSize.y, 1);
            blockBehaviour.Construct(block);
        }));
    }
}