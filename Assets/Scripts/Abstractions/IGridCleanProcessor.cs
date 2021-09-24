using System.Collections.Generic;
using UnityEngine;

public interface IGridCleanProcessor
{
    void ProcessAfterCleaning(Dictionary<Vector2Int, Block> positionStuckBlocks, Vector2Int[] cleanedPositions);
}