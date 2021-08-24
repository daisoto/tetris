using UnityEngine;

[CreateAssetMenu(fileName = "New TetrisSettingsData", menuName = "Tetris settings data")]
public class TetrisSettingsData : ScriptableObject
{
    public Vector2Int size
    {
        get
        {
            Vector2Int finalSize = _size;
            Vector2Int maxTetrominoSize = GetMaxTetrominoSize();

            if (maxTetrominoSize.x > _size.x)
            {
                finalSize.x = maxTetrominoSize.x;
            }
            if (maxTetrominoSize.y > _size.y)
            {
                finalSize.y = maxTetrominoSize.y;
            }

            return finalSize;
        }
    }

    public Vector2Int initialPosition
    {
        get
        {
            return new Vector2Int(size.x / 2, size.y);
        }
    }

    public TetrominoData[] tetrominoDatas { get => _tetrominoDatas; }

    public RoundData[] roundDatas { get => _roundDatas; }

    [SerializeField] private RoundData[] _roundDatas;

    [SerializeField] private TetrominoData[] _tetrominoDatas;

    [SerializeField] private Vector2Int _size = new Vector2Int(10, 20);

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
