using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private int _gems;
    [SerializeField] private int _highScore;
    [SerializeField] private string _playerName;
    [SerializeField] private int _numberOfGame;
    [SerializeField] private int _highestBlockIndex;
    [SerializeField] private int _level;

    public int Gems { get => _gems; set => _gems = value; }
    public int HighScore { get => _highScore; set => _highScore = value; }
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public int NumberOfGame { get => _numberOfGame; set => _numberOfGame = value; }
    public int HighestBlockIndex { get => _highestBlockIndex; set => _highestBlockIndex = value; }
    public int Level { get => _level; set => _level = value; }

    public PlayerData()
    {
        _gems = 0;
        _highScore = 0;
        _playerName = "New Player";
        _numberOfGame = 0;
        _highestBlockIndex = 0;
        _level = 0;
    }
}
