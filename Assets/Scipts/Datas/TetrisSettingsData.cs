using UnityEngine;

[CreateAssetMenu(fileName = "New TetrisSettingsData", menuName = "Tetris settings data")]
public class TetrisSettingsData : ScriptableObject
{
    public Vector2Int size { get => _size; }
    public TetrominoData[] tetrominoDatas { get => _tetrominoDatas; }
    public RoundData[] roundData { get => _roundDatas; }

    [SerializeField] private RoundData[] _roundDatas;
    [SerializeField] private TetrominoData[] _tetrominoDatas;
    [SerializeField] private Vector2Int _size;
}
