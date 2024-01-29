using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class MapData
{
    [SerializeField] private Dictionary<Vector2Int, int> _levelData;
    [SerializeField] private int _nextBlock;
    [SerializeField] private int _secondNextBlock;
    [SerializeField] private BigInteger _score = 0;


    public int NextBlock { get => _nextBlock; set => _nextBlock = value; }
    public int SecondNextBlock { get => _secondNextBlock; set => _secondNextBlock = value; }
    public BigInteger Score { get => _score; set => _score = value; }
    public Dictionary<Vector2Int, int> LevelData { get => _levelData; set => _levelData = value; }
}
