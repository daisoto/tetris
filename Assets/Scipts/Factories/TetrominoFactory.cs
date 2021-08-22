using UnityEngine;

public class TetrominoFactory : IFactory<Tetromino>
{
    private TetrominoData tetrominoData = null;

    private Block[] blocks = null;

    private Vector2Int initialPosition = default;

    public TetrominoFactory(Vector2Int initialPosition)
    {
        this.initialPosition = initialPosition;
    }

    public Tetromino Create()
    {
        if (blocks == null || tetrominoData == null)
        {
            return null;
        }
        else
        {
            return new Tetromino(blocks);
        }
    }

    public void SetTetrominoData(TetrominoData tetrominoData)
    {
        this.tetrominoData = tetrominoData;
    }

    public void SetBlocks(Block[] blocks)
    {
        this.blocks = blocks;

        if (tetrominoData == null)
        {
            return;
        }
        else
        {
            SetBlocksPosition();
        }
    }

    private void SetBlocksPosition()
    {
        if (tetrominoData.capacity != blocks.Length)
        {
            return;
        }

        Vector2Int lowerCenter = new Vector2Int(tetrominoData.size.y / 2, tetrominoData.size.y);

        for (int i = 0; i < tetrominoData.size.x; i++)
        {
            for (int j = 0; j < tetrominoData.size.y; j++)
            {
                if (tetrominoData.shape[i, j])
                {
                    blocks[tetrominoData.size.y * i + j].position.Value = initialPosition + new Vector2Int(i, j) - lowerCenter;
                }
            }
        }
    }
}