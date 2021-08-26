using UniRx;
using UnityEngine;

public class BlockBehavioursGenerator : ConstructableBehaviour<Vector2Int>
{
    [SerializeField] private RoundsManager roundsManager = null;
    [SerializeField] private BlockBehaviour blockBehaviourPrefab = null;
    [SerializeField] private SpriteRenderer gridRenderer = null;

    private Vector2 blockSize = default;
    private Vector2 blockCenter = default;
    private Vector2 zeroPosition = default;

    public override void Construct(Vector2Int gridSize)
    {
        blockSize = gridRenderer.bounds.size / new Vector2(gridSize.x, gridSize.y);
        blockCenter = blockSize / 2;
        zeroPosition = - gridRenderer.bounds.size / 2;

        base.Construct(gridSize);
    }

    protected override void Subscribe()
    {
        disposablesContainer.Add(roundsManager.currentTetromino.Subscribe(tetromino =>
        {
            if (tetromino == null)
            {
                return;
            }

            foreach (Block block in tetromino.blocks)
            {
                BlockBehaviour blockBehaviour = Instantiate(blockBehaviourPrefab, transform);
                blockBehaviour.Construct(block, blockSize, blockCenter, zeroPosition);
            }
        }));
    }
}