using UnityEngine;

[CreateAssetMenu(fileName = "New TetrominoeData", menuName = "Tetromino data")]
public class TetrominoData : ScriptableObject
{
    public bool[,] shape = default;

    public BlockData blockData { get => _blockData; }

    [SerializeField] private BlockData _blockData = default;
}

