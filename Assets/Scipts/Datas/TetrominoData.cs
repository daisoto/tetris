using UnityEngine;

[CreateAssetMenu(fileName = "New TetrominoData", menuName = "Tetromino data")]
public class TetrominoData : ScriptableObject
{
    public SerializableRectangularArray<bool> shape { get => _shape; }

    [SerializeField, HideInInspector]  private SerializableRectangularArray<bool> _shape = null;

    public int GetBlocksNum()
    {
        int num = 0;

        for (int i = 0; i < shape.size.x; i++)
        {
            for (int j = 0; j < shape.size.y; j++)
            {
                if (shape[i, j])
                {
                    num++;
                }
            }
        }

        return num;
    }
}

