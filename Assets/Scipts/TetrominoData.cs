using UnityEngine;

[CreateAssetMenu(fileName = "New TetrominoData", menuName = "Tetromino data")]
public class TetrominoData : ScriptableObject
{
    public SerializableRectangularArray<bool> shape { get => _shape; }

    public BlockData blockData { get => _blockData; }

    [SerializeField] private BlockData _blockData = default;

    [SerializeField, HideInInspector]  private SerializableRectangularArray<bool> _shape = null;
}

