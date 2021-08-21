using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TetrisGrid
{
    public Vector2Int size = default;

    public ReactiveCommand<Block> onInsert = new ReactiveCommand<Block>();
    public ReactiveCommand onClear = new ReactiveCommand();

    private bool[,] grid = null;

    private IGridChecker gridChecker = null;

    private Dictionary<Vector2Int, Block> cellBlocks = new Dictionary<Vector2Int, Block>();

    private DisposablesContainer disposablesContainer = new DisposablesContainer();

    public TetrisGrid(Vector2Int size, IGridChecker gridChecker)
    {
        this.size = size;
        this.gridChecker = gridChecker;

        grid = new bool[size.x, size.y];

        disposablesContainer.Add(onInsert.Subscribe(block =>
        {
            if (IsRowColumnFilled(block.position.Value, out Vector2Int[] positionsToClear))
            {
                Array.ForEach(positionsToClear, position => Clear(position));
                onClear.Execute();
            }
        }));

        disposablesContainer.Add(onClear.Subscribe(_ => 
        {
            // TODO: ������� ��������� ������ (������� � IGridChecker), ����� ������ �� �� �����
        }));
    }

    public bool TryToInsert(Block block)
    {
        if (IsNextSpaceFree(block.position.Value))
        {
            Insert(block);
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Insert(Block block)
    {
        Vector2Int position = block.position.Value;

        cellBlocks.Add(position, block);

        grid[position.x, position.y] = true;

        onInsert.Execute(block);
    }

    public void Clear(Vector2Int point)
    {
        cellBlocks[point].isAlive.Value = false;
        cellBlocks.Remove(point);
        grid[point.x, point.y] = false;
    }

    public bool IsRowColumnFilled(Vector2Int position, out Vector2Int[] positionsToClear)
    {
        return gridChecker.IsRowColumnFilled(position, grid, out positionsToClear);
    }

    public bool IsNextSpaceFree(Vector2Int position)
    {
        return gridChecker.IsNextSpaceFree(position, grid);
    }

    public bool IsSpaceFree(Vector2Int position)
    {
        return !grid[position.x, position.y];
    }
}
