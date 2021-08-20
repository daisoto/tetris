using UnityEngine;

[CreateAssetMenu(fileName = "New TetrisSettingsData", menuName = "Tetris settings data")]
public class TetrisSettingsData : ScriptableObject
{

    public TetrominoData[] tetrominoDatas { get => tetrominoDatas; }

    public int tetrominosInRound { get { return _tetrominosInRound < minTetrominosInRound ? minTetrominosInRound : _tetrominosInRound; } }
    public float initialFallPeriod { get { return _initialFallPeriod < minFallPeriod ? minFallPeriod : _initialFallPeriod; } }


    [SerializeField] private TetrominoData[] _tetrominoDatas = default;

    [Space]
    [SerializeField] private int minTetrominosInRound = 1;
    [SerializeField] private float minFallPeriod = 0.1f;
    
    [Space]
    [SerializeField] private int _tetrominosInRound = 10;

    [Header("Values are set in seconds")]
    [SerializeField] private float _initialFallPeriod = 0.5f;
    [SerializeField] private float _fallPeriodDecrement = 0.02f;
}
