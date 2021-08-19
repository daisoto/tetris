using UniRx;
using UnityEngine;

public class TetrominoBehaviour : ConstructableBehaviour<ITetromino>, ITetromino
{
    public ReactiveProperty<Vector2Int> currentAxis => throw new System.NotImplementedException();

    public void Move() // TODO : �������� ������ ����� �.�. ���� �������� �� ��������������� � ������, � ��� ����� ���� ��� ���������������� � ����
    {
        if (isConstructed)
        {
            model.Move();
        }
    }

    public void Rotate()
    {
        if (isConstructed)
        {
            model.Rotate();
        }
    }

    public void TickMove()
    {
        if (isConstructed)
        {
            model.TickMove();
        }
    }
}
