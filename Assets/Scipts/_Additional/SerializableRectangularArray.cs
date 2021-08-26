using System;
using UnityEngine;

[Serializable]
public class SerializableRectangularArray<T>
{
    public Vector2Int size { get => _size; }

    public int capacity { get => _values.Length; }

    [SerializeField, HideInInspector] private T[] _values;

    [SerializeField, HideInInspector]  private Vector2Int _size;

    public static implicit operator SerializableRectangularArray<T>(T[][] rawArray)
    {
        return new SerializableRectangularArray<T>(rawArray);
    }

    public static implicit operator SerializableRectangularArray<T>(T[,] rawArray)
    {
        return new SerializableRectangularArray<T>(rawArray);
    }

    public static implicit operator T[,](SerializableRectangularArray<T> serializableRectangularArray)
    {
        T[,] value = new T[serializableRectangularArray.size.x, serializableRectangularArray.size.y];

        for (int i = 0; i < serializableRectangularArray.size.x; i++)
        {
            for (int j = 0; j < serializableRectangularArray.size.y; j++)
            {
                value[i, j] = serializableRectangularArray[i, j];
            }
        }

        return value;
    }

    public SerializableRectangularArray()
    {
        _size = Vector2Int.zero;
    }

    public SerializableRectangularArray(Vector2Int size)
    {
        _size = size;

        _values = new T[size.x * size.y];
    }

    public SerializableRectangularArray(Vector2Int size, T[] rawArray)
    {
        _size = size;

        _values = (T[])rawArray.Clone();
    }

    public SerializableRectangularArray(int rowsNum, int columnsNum)
    {
        _size = new Vector2Int(rowsNum, columnsNum);

        _values = new T[_size.x * _size.y];
    }

    public SerializableRectangularArray(int rowsNum, int columnsNum, T[] rawArray)
    {
        _size = new Vector2Int(rowsNum, columnsNum);

        _values = (T[])rawArray.Clone();
    }

    public SerializableRectangularArray(T[,] rawArray)
    {
        _size = new Vector2Int(rawArray.GetLength(0), rawArray.GetLength(0));

        _values = new T[_size.x * _size.y];

        SetValues(rawArray);
    }

    public SerializableRectangularArray(T[][] rawArray)
    {
        _size = new Vector2Int(rawArray.Length, rawArray[0].Length);

        _values = new T[_size.x * _size.y];

        SetValues(rawArray);
    }

    ~SerializableRectangularArray()
    {
        Array.Clear(_values, 0, _values.Length);
        _values = null;
    }

    public T this[int i, int j]
    {
        get
        {
            if (i > _size.x || j > _size.y)
            {
                throw new IndexOutOfRangeException();
            }
            else if (i * j > _values.Length)
            {
                throw new NullReferenceException();
            }
            else
            {
                return _values[_size.y * i + j];
            }
        }
        set
        {
            if (i > _size.x || j > _size.y)
            {
                throw new IndexOutOfRangeException();
            }
            else if (i * j > _values.Length)
            {
                throw new NullReferenceException();
            }
            else
            {
                _values[_size.y * i + j] = value;
            }
        }
    }

    private void SetValues(T[,] rawArray)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                this[i, j] = rawArray[i, j];
            }
        }
    }

    private void SetValues(T[][] rawArray)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                this[i, j] = rawArray[i][j];
            }
        }
    }
}