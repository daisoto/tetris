using UniRx;
using UnityEngine;

public class BlockBehavioursGenerator: ConstructableBehaviour<BlocksFactory>
{
    [SerializeField] private Transform blocksContainer = null;
    [SerializeField] private BlockBehaviour blockBehaviourPrefab = null;

    protected override void OnEnable()
    {
        disposablesContainer.Add(model.OnBlockCreate.Subscribe(block =>
        {
            BlockBehaviour blockBehaviour = Instantiate(blockBehaviourPrefab, blocksContainer);
            blockBehaviour.Construct(block);
        }));
    }
}