using UnityEngine;

[CreateAssetMenu(fileName = "New TetrominoData", menuName = "Tetromino data")]
public class TetrominoData : ScriptableObject
{
    public SerializableRectangularArray<bool> shape
    {
        get
        {
            if (_shape == null)
            {
                _shape = new SerializableRectangularArray<bool>(4, 4, editorArray);
            }

            return _shape;
        }
    }

    public BlockData blockData { get => _blockData; }

    [SerializeField, HideInInspector] private bool[] editorArray = new bool[16];

    [SerializeField] private BlockData _blockData = default;

    private SerializableRectangularArray<bool> _shape = null;
}

