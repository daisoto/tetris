using UnityEngine;

[CreateAssetMenu(fileName = "New TetrominoeData", menuName = "Tetromino data")]
public class TetrominoData : ScriptableObject
{
    public SerializableRectangularArray<bool> shape { get => _shape; }

    public BlockData blockData { get => _blockData; }

    [SerializeField, HideInInspector] private SerializableRectangularArray<bool> _shape = default;

    [SerializeField] private BlockData _blockData = default;
}

