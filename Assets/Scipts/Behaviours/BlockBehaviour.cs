using UniRx;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlockBehaviour : ConstructableBehaviour<Block>
{
    private SpriteRenderer spriteRenderer = null;

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    public override void Construct(Block block)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        base.Construct(block);
    }

    private void OnEnable()
    {
        if (isConstructed)
        {
            disposablesContainer.Add(model.position.Subscribe(position =>
            {
                transform.position = new Vector3(position.x, position.y, 0);
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

    private void OnDisable()
    {
        disposablesContainer.Clear();
    }
}
