using UniRx;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlockBehaviour : ConstructableBehaviour<Block>
{
    private SpriteRenderer spriteRenderer = null;

    private float zPosition = default;

    public override void Construct(Block block)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        zPosition = transform.position.z;

        base.Construct(block);
    }

    protected override void OnEnable()
    {
        if (isConstructed)
        {
            disposablesContainer.Add(model.position.Subscribe(position =>
            {
                transform.position = new Vector3(position.x, position.y, zPosition);
            }));

            disposablesContainer.Add(model.color.Subscribe(color =>
            {
                spriteRenderer.color = color;
            }));

            disposablesContainer.Add(model.isAlive.Subscribe(isAlive =>
            {
                if (!isAlive)
                {
                    Destroy(this);
                }
            }));
        }
    }    
}
