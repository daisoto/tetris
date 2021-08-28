using UnityEngine;

[CreateAssetMenu(fileName = "New RoundData", menuName = "Round data")]
public class RoundData: ScriptableObject
{
    public BlockData blockData { get => _blockData; }

    public int tetrominosInRound { get => _tetrominosInRound; }

    public float fallPeriod { get => _fallPeriod; }

    [SerializeField] private BlockData _blockData;

    [SerializeField, Range(5, 100)] private int _tetrominosInRound = 50;

    [SerializeField, Range(0.1f, 1f)] private float _fallPeriod = 0.5f;
}