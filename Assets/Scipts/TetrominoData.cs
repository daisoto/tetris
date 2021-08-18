using UnityEngine;

[CreateAssetMenu(fileName = "New TetrominoeData", menuName = "Tetromino data")]
public class TetrominoData : ScriptableObject
{
    public bool[,] shape = default; // TODO: custom editor
}
