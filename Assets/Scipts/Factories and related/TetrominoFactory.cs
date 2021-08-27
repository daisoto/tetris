using System.Collections.Generic;
using UnityEngine;

public class TetrominoFactory : IFactory<Tetromino>
{
    private TetrominoData[] tetrominoDatas = null;

    private Vector2Int initialPosition = default;

    private IPool<Block> blocksPool = null;

    public TetrominoFactory(Vector2Int gridSize, IPool<Block> blocksPool, TetrominoData[] tetrominoDatas)
    {
        this.tetrominoDatas = tetrominoDatas;
        this.blocksPool = blocksPool;

        initialPosition = new CeilInitialPositionProvider().GetInitialPosition(gridSize);
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
        //int xCenter = Misc.GetBankRounded(tetrominoData.size.x / 2f);

        //Vector2Int axis = initialPosition + new Vector2Int(xCenter, 0);

        int blockIndex = 0;

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