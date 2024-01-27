using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class MapData
{
    private Dictionary<Vector2Int, Block> _levelData = new Dictionary<Vector2Int, Block>();
    private Block _nextBlock;
    private Block _secondNextBlock;
    private BigInteger _score = 0;

    public Dictionary<Vector2Int, Block> LevelData { get => _levelData; set => _levelData = value; }
    public Block NextBlock { get => _nextBlock; set => _nextBlock = value; }
    public Block SecondNextBlock { get => _secondNextBlock; set => _secondNextBlock = value; }
    public BigInteger Score { get => _score; set => _score = value; }
}
