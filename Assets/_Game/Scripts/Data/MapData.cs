using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class MapData
{
    [SerializeField] private DictionaryLib.Block_Coor_Dic _levelData;
    [SerializeField] private Block _nextBlock;
    [SerializeField] private Block _secondNextBlock;
    [SerializeField] private BigInteger _score = 0;

    public IDictionary<Vector2Int, Block> LevelData
    {
        get { return _levelData; }
        set { _levelData.CopyFrom(value); }
    }
    public Block NextBlock { get => _nextBlock; set => _nextBlock = value; }
    public Block SecondNextBlock { get => _secondNextBlock; set => _secondNextBlock = value; }
    public BigInteger Score { get => _score; set => _score = value; }
}
