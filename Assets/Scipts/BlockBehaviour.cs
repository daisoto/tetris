using UniRx;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlockBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;

    private Block block = null;

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    private bool isConstructed = false;

    public void Construct(Block block)
    {
        this.block = block;
        spriteRenderer = GetComponent<SpriteRenderer>();

        isConstructed = true;
    }

    private void OnEnable()
    {
        if (isConstructed)
        {
            disposablesContainer.Add(block.position.Subscribe(position =>
            {
                transform.position = new Vector3(position.x, position.y, 0);
            }));

            disposablesContainer.Add(block.color.Subscribe(color =>
            {
                spriteRenderer.color = color;
            }));

            disposablesContainer.Add(block.isAlive.Subscribe(isAlive =>
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
