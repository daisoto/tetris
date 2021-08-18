using UnityEngine;
using System;

public class RightAngleRotator : IRotator
{
    private const double rightAngle = Math.PI / 2;
    
    public Vector2Int GetRotatedPosition(Vector2Int position, Vector2Int axis)
    {
        double rawX = axis.x + (position.x - axis.x) * Math.Cos(rightAngle) - (position.y - axis.y) * Math.Sin(rightAngle);
        double rawY = axis.y + (position.x - axis.x) * Math.Sin(rightAngle) + (position.y - axis.y) * Math.Cos(rightAngle);

        int x = Convert.ToInt32(Math.Round(rawX));
        int y = Convert.ToInt32(Math.Round(rawY));

        return new Vector2Int(x, y);
    }
}
