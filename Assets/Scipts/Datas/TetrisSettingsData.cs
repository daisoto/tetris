using UnityEngine;

[CreateAssetMenu(fileName = "New TetrisSettingsData", menuName = "Tetris settings data")]
public class TetrisSettingsData : ScriptableObject
{
    public Vector2Int size { get => _size; }

    public Vector2Int initialPosition { get => _initialPosition; }

    public TetrominoData[] tetrominoDatas { get => _tetrominoDatas; }

    public RoundData[] roundDatas { get => _roundDatas; }

    [SerializeField] private RoundData[] _roundDatas;

    [SerializeField] private TetrominoData[] _tetrominoDatas;

    [SerializeField] private Vector2Int _size = new Vector2Int(10, 20);

    [SerializeField] private Vector2Int _initialPosition = new Vector2Int(5, -1);
}
