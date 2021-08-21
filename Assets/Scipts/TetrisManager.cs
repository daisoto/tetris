using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

public class TetrisManager : MonoBehaviour
{
    [SerializeField] private TetrisSettingsData tetrisSettings = null;

    private List<Block> blocks = new List<Block>();

    private void GetBlock(BlockData blockData)
    {
        Block block = new Block();
    }
}
