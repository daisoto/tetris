using UnityEngine;

public class SpriteMaskScaler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetRenderer = null;
    [SerializeField] private SpriteMask spriteMask = null;

    private void Awake()
    {
        Scale();
    }

    [ContextMenu("Scale")]
    private void Scale()
    {
        spriteMask.transform.localScale = targetRenderer.size / spriteMask.sprite.bounds.size;
    }
}
