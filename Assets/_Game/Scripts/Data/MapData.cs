using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    [SerializeField] private DictionaryLib.Map_Data _levelData;
    [SerializeField] private int _nextBlock;
    [SerializeField] private int _secondNextBlock;
    [SerializeField] private string _score;


    public int NextBlock { get => _nextBlock; set => _nextBlock = value; }
    public int SecondNextBlock { get => _secondNextBlock; set => _secondNextBlock = value; }
    public string Score { get => _score; set => _score = value; }
    public IDictionary<SerializeVector2Int, int> LevelData
    {
        get { return _levelData; }
        set { _levelData.CopyFrom(value); }
    }

    public MapData()
    {
        _levelData = new DictionaryLib.Map_Data();
        _nextBlock = 0;
        _secondNextBlock = 0;
        _score = "0";
    }
}
