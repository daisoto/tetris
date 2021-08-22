using UnityEngine;

[CreateAssetMenu(fileName = "New BlocksData", menuName = "Blocks data")]
public class BlockData : ScriptableObject
{
    public Sprite sprite { get => _sprite; }

    [SerializeField] private Sprite _sprite = null;

    public Color color { get => _color; }

    [SerializeField] private Color _color = default;
}
