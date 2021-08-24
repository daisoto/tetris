using UniRx;
using UnityEngine;

public class BlockBehavioursGenerator: ConstructableBehaviour<BlocksFactory>
{
    [SerializeField] private BlockBehaviour blockBehaviourPrefab = null;

    protected override void OnEnable()
    {
        disposablesContainer.Add(model.OnBlockCreate.Subscribe(block =>
        {
            BlockBehaviour blockBehaviour = Instantiate(blockBehaviourPrefab, transform);
            blockBehaviour.Construct(block);
        }));
    }
}