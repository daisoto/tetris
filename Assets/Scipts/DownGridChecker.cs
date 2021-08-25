using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DownGridChecker : GridChecker
{
    public DownGridChecker(bool[,] grid) : base(grid) { }

    public override bool IsDefaultSpaceFree(Vector2Int position)
    {
        return IsDownSpaceFree(position);
    }

    public override int GetScoreFromFilled(Vector2Int[] insertedPositions, out Vector2Int[] positionsToClear)
    {
        List<Vector2Int> positionsToClearList = new List<Vector2Int>();

        int score = 0;

        int[] yDistinctPositions = insertedPositions.Select(position => position.y).Distinct().ToArray();

        foreach (int yPosition in yDistinctPositions)
        {
            bool isFilled = true;
            List<Vector2Int> rowPositionsToClear = new List<Vector2Int>();

            for (int xPosition = 0; xPosition < gridSize.x; xPosition++)
            {
                if (!grid[xPosition, yPosition])
                {
                    rowPositionsToClear.Clear();
                    isFilled = false;
                    break;
                }

                rowPositionsToClear.Add(new Vector2Int(xPosition, yPosition));
            }

            if (isFilled)
            {
                score++;
                positionsToClearList.AddRange(rowPositionsToClear);
            }
        }

        positionsToClear = positionsToClearList.ToArray();

        return score;
    }
}
