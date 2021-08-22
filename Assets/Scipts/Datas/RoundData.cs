using UnityEngine;

public class RoundData: ScriptableObject
{
    [SerializeField] private BlockData blockData;

    [SerializeField, Range(5, 100)] private int tetrominosInRound;

    [SerializeField, Range(0.1f, 1f)] private float fallPeriod;
}