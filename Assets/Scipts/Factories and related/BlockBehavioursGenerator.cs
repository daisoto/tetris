using UniRx;
using UnityEngine;

public class BlockBehavioursGenerator: ConstructableBehaviour<BlocksFactory>
{
    [SerializeField] private BlockBehaviour blockBehaviourPrefab = null;
    [SerializeField] private SpriteRenderer gridRenderer = null;

    private Vector2 blockSize = default;
    private Vector2 blockCenter = default;
    private Vector2 zeroPosition = default;

    public void Construct(BlocksFactory blocksFactory, Vector2Int gridSize)
    {
        blockSize = gridRenderer.bounds.size / new Vector2(gridSize.x, gridSize.y);
        blockCenter = blockSize / 2;
        zeroPosition = - gridRenderer.bounds.size / 2;

        Construct(blocksFactory);
    }

    protected override void OnEnable()
    {
        if (isConstructed)
        {
            disposablesContainer.Add(model.OnBlockCreate.Subscribe(block =>
            {
                BlockBehaviour blockBehaviour = Instantiate(blockBehaviourPrefab, transform);
                blockBehaviour.Construct(block, blockSize, blockCenter, zeroPosition);
            }));
        }
    }
}