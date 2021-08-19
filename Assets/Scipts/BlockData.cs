using UnityEngine;

public class BlockData : ScriptableObject
{
    public Sprite sprite { get => _sprite; }

    [SerializeField] private Sprite _sprite = null;

    public Color color { get => _color; }

    [SerializeField] private Color _color = Color.white;
}
