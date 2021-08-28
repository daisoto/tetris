using System.Collections.Generic;
using UnityEngine;

public class TetrominoFactory : IFactory<Tetromino>
{
    private TetrominoData[] tetrominoDatas = null;

    private IPool<Block> blocksPool = null;

    private IInitialPositionProvider initialPositionProvider = null;

    public TetrominoFactory(Vector2Int gridSize, IPool<Block> blocksPool, TetrominoData[] tetrominoDatas)
    {
        this.tetrominoDatas = tetrominoDatas;
        this.blocksPool = blocksPool;

        initialPositionProvider = new CeilInitialPositionProvider(gridSize);
    }

    public Tetromino Create()
    {
        TetrominoData tetrominoData = tetrominoDatas[Random.Range(0, tetrominoDatas.Length)];

        List<Block> blocks = new List<Block>();

        for (int i = 0; i < tetrominoData.GetBlocksNum(); i++)
        {
            blocks.Add(blocksPool.Get());
        }

        SetBlocksPosition(tetrominoData, blocks.ToArray());

        Tetromino tetromino = new Tetromino(blocks.ToArray(), tetrominoData.shape);

        return tetromino;
    }

    private void SetBlocksPosition(TetrominoData tetrominoData, Block[] blocks)
    {
        int blockIndex = 0;

        Vector2Int initialPosition = initialPositionProvider.GetInitialPosition(tetrominoData.size);

        for (int i = 0; i < tetrominoData.size.x; i++)
        {
            for (int j = 0; j < tetrominoData.size.y; j++)
            {
                if (tetrominoData.shape[i, j])
                {
                    blocks[blockIndex].position.Value = initialPosition + new Vector2Int(j, i);
                    blockIndex++;
                }
            }
        }
    }
}