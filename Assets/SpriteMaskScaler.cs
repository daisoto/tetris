using UnityEngine;

[RequireComponent(typeof(SpriteMask))]
public class SpriteMaskScaler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetRenderer = null;

    private void Awake()
    {
        Scale();
    }

    [ContextMenu("Scale")]
    private void Scale()
    {
        SpriteMask spriteMask = GetComponent<SpriteMask>();

        Debug.Log(targetRenderer.size);
        Debug.Log(spriteMask.sprite.bounds.size);

        transform.localScale = targetRenderer.size / spriteMask.sprite.bounds.size;
    }
}
