using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGrid
{
    public Vector2Int size { get; private set; } = default;

    private bool[,] cells = null;

    public TetrisGrid(Vector2Int size)
    {
        this.size = size;

        cells = new bool[size.x, size.y];
    }

    public bool TryToInsert(Vector2Int[] indexes)
    {
        // TODO: true если встал на месте
        return true;
    }

    public void Insert(Vector2Int[] indexes)
    {
       //TODO: проверка заполненности ряда
    }
}
