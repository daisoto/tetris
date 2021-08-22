using UnityEngine;

[CreateAssetMenu(fileName = "New TetrisSettingsData", menuName = "Tetris settings data")]
public class TetrisSettingsData : ScriptableObject
{

    public TetrominoData[] tetrominoDatas { get => _tetrominoDatas; }
    public RoundData[] roundData { get => _roundDatas; }

    [SerializeField] private RoundData[] _roundDatas;
    [SerializeField] private TetrominoData[] _tetrominoDatas;
}
