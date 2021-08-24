using System.Collections.Generic;
using UnityEngine;

public class TetrominoFactory : IFactory<Tetromino>
{
    private TetrominoData[] tetrominoDatas = null;

    private Vector2Int initialPosition = default;

    private TetrisGrid tetrisGrid = null;

    private IPool<Block> blocksPool = null;

    public TetrominoFactory(Vector2Int initialPosition, TetrisGrid tetrisGrid, IPool<Block> blocksPool, TetrominoData[] tetrominoDatas)
    {
        this.tetrominoDatas = tetrominoDatas;
        this.blocksPool = blocksPool;
        this.tetrisGrid = tetrisGrid;
        this.initialPosition = initialPosition;
    }

    public Tetromino Create()
    {
        TetrominoData tetrominoData = tetrominoDatas[TetrisRandom.random.Next(tetrominoDatas.Length)];

        List<Block> blocks = new List<Block>();

        for (int i = 0; i < tetrominoData.capacity; i++)
        {
            blocks.Add(blocksPool.Get());
        }

        SetBlocksPosition(tetrominoData, blocks.ToArray());

        return new Tetromino(tetrisGrid, blocks.ToArray());
    }

    private void SetBlocksPosition(TetrominoData tetrominoData, Block[] blocks)
    {
        Vector2Int currentCenter = initialPosition + new Vector2Int(tetrominoData.size.x / 2, tetrominoData.size.y);

        for (int i = 0; i < tetrominoData.size.x; i++)
        {
            for (int j = 0; j < tetrominoData.size.y; j++)
            {
                if (tetrominoData.shape[i, j])
                {
                    blocks[tetrominoData.size.y * i + j].position.Value = currentCenter + new Vector2Int(i, j);
                }
            }
        }
    }
}