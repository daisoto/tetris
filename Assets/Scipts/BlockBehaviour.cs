using DG.Tweening;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlockBehaviour : ConstructableBehaviour<Block>
{
    [SerializeField] private float fadeDuration = 0.1f;

    private SpriteRenderer spriteRenderer = null;

    private Vector2 blockSize = default;
    private Vector2 blockCenter = default;
    private Vector2 zeroPosition = default;

    public void Construct(Block block, Vector2 blockSize, Vector2 blockCenter, Vector2 zeroPosition)
    {
        this.blockSize = blockSize;
        this.blockCenter = blockCenter;
        this.zeroPosition = zeroPosition;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = blockSize;

        base.Construct(block);
    }

    protected override void OnEnable()
    {
        if (isConstructed)
        {
            disposablesContainer.Add(model.position.Subscribe(position =>
            {
                transform.localPosition = GetBlockCoord(position);
            }));

            disposablesContainer.Add(model.color.Subscribe(color =>
            {
                spriteRenderer.color = color;
            }));

            disposablesContainer.Add(model.sprite.Subscribe(sprite =>
            {
                spriteRenderer.sprite = sprite;
            }));

            disposablesContainer.Add(model.isStuck.Subscribe(isStuck =>
            {
                if (isStuck)
                {
                    if (transform != null)
                    {
                        transform.DOScale(1.1f, fadeDuration / 2).OnComplete(() =>
                        {
                            if (transform != null)
                            {
                                transform.DOScale(1, fadeDuration / 2);
                            }
                        });
                    }
                }
            }));

            disposablesContainer.Add(model.isAlive.Subscribe(isAlive =>
            {
                if (!isAlive)
                {
                    spriteRenderer?.DOFade(0, fadeDuration).OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                }
            }));
        }
    }

    private Vector2 GetBlockCoord(Vector2Int gridPosition)
    {
        return blockSize * gridPosition + blockCenter + zeroPosition;
    }
}