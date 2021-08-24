using UniRx;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoFactory : IFactory<Tetromino>
{
    public ReactiveCommand<Tetromino> OnTetrominoCreated = new ReactiveCommand<Tetromino>();

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

        for (int i = 0; i < tetrominoData.GetBlocksNum(); i++)
        {
            blocks.Add(blocksPool.Get());
        }

        SetBlocksPosition(tetrominoData, blocks.ToArray());

        Tetromino tetromino = new Tetromino(tetrisGrid, blocks.ToArray());

        OnTetrominoCreated.Execute(tetromino);

        return tetromino;
    }

    private void SetBlocksPosition(TetrominoData tetrominoData, Block[] blocks)
    {
        Vector2Int axis = initialPosition + new Vector2Int(tetrominoData.size.x / 2, 0);

        int blockIndex = 0;

        for (int i = 0; i < tetrominoData.size.x; i++)
        {
            for (int j = 0; j < tetrominoData.size.y; j++)
            {
                if (tetrominoData.shape[i, j])
                {
                    blocks[blockIndex].position.Value = axis + new Vector2Int(i, j);
                    blockIndex++;
                }
            }
        }
    }
}