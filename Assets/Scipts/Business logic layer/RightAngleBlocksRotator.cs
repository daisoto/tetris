using UnityEngine;
using System;
using System.Collections.Generic;

public class RightAngleBlocksRotator : BlocksRotator
{
    private const double rightAngle = Math.PI / 2;

    public RightAngleBlocksRotator(IGridChecker gridChecker) : base(gridChecker) { }

    public override void Rotate(Block[] blocks)
    {
        Dictionary<Block, Vector2Int> blocksNewPositions = new Dictionary<Block, Vector2Int>();

        Vector2Int axis = GetAxis(blocks);

        foreach (Block block in blocks)
        {
            Vector2Int newPosition = GetRotatedPosition(block.position.Value, axis);

            if (!gridChecker.IsFit(newPosition))
            {
                return;
            }

            blocksNewPositions.Add(block, newPosition);
        }

        foreach (Block block in blocks)
        {
            block.position.Value = blocksNewPositions[block];
        }
    }

    private Vector2Int GetRotatedPosition(Vector2Int position, Vector2Int axis)
    {
        double rawX = axis.x + (position.x - axis.x) * Math.Cos(rightAngle) - (position.y - axis.y) * Math.Sin(rightAngle);
        double rawY = axis.y + (position.x - axis.x) * Math.Sin(rightAngle) + (position.y - axis.y) * Math.Cos(rightAngle);

        int x = Convert.ToInt32(Math.Round(rawX));
        int y = Convert.ToInt32(Math.Round(rawY));

        return new Vector2Int(x, y);
    }

    private Vector2Int GetAxis(Block[] blocks)
    {
        int xMin = blocks[0].position.Value.x;
        int yMin = blocks[0].position.Value.y;
        int yMax = blocks[0].position.Value.y;
        int xMax = blocks[0].position.Value.x;

        foreach (Block block in blocks)
        {
            if (block.position.Value.x < xMin)
            {
                xMin = block.position.Value.x;
            }
            else if (block.position.Value.x > xMax)
            {
                xMax = block.position.Value.x;
            }

            if (block.position.Value.y < yMin)
            {
                yMin = block.position.Value.y;
            }
            else if (block.position.Value.y > yMax)
            {
                yMax = block.position.Value.y;
            }
        }

        float xAxis = (xMax + xMin) / 2.0f;
        float yAxis = (yMax + yMin) / 2.0f;

        return new Vector2Int(Misc.GetBankRounded(xAxis), Misc.GetBankRounded(yAxis));
    }

    
}
