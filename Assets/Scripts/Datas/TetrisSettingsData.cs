using UnityEngine;

[CreateAssetMenu(fileName = "New TetrisSettingsData", menuName = "Tetris settings data")]
public class TetrisSettingsData : ScriptableObject
{
    public Vector2Int gridSize
    {
        get
        {
            Vector2Int finalSize = _gridSize;
            Vector2Int maxTetrominoSize = GetMaxTetrominoSize();

            if (maxTetrominoSize.x > _gridSize.x)
            {
                finalSize.x = maxTetrominoSize.x;
            }
            if (maxTetrominoSize.y > _gridSize.y)
            {
                finalSize.y = maxTetrominoSize.y;
            }

            return finalSize;
        }
    }

    public float inputUpdateTime { get => _inputUpdateTime; }

    public TetrominoData[] tetrominoDatas { get => _tetrominoDatas; }

    public RoundData[] roundDatas { get => _roundDatas; }

    [SerializeField] private RoundData[] _roundDatas;

    [SerializeField] private TetrominoData[] _tetrominoDatas;

    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 20);

    [SerializeField] private float _inputUpdateTime = 0.1f;

    public Vector2Int GetMaxTetrominoSize()
    {
        Vector2Int maxTetrominoSize = Vector2Int.zero;

        foreach (TetrominoData tetrominoData in _tetrominoDatas)
        {
            if (tetrominoData.size.x > maxTetrominoSize.x)
            {
                maxTetrominoSize.x = tetrominoData.size.x;
            }

            if (tetrominoData.size.y > maxTetrominoSize.y)
            {
                maxTetrominoSize.y = tetrominoData.size.y;
            }
        }

        return maxTetrominoSize;
    }
}
