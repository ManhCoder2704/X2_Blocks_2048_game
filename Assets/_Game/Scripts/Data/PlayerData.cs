using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private int _gems;
    [SerializeField] private int _highScore;
    [SerializeField] private string _playerName;
    [SerializeField] private int _highestBlockIndex;

    public int Gems { get => _gems; set => _gems = value; }
    public int HighScore { get => _highScore; set => _highScore = value; }
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public int HighestBlockIndex { get => _highestBlockIndex; set => _highestBlockIndex = value; }

    public PlayerData()
    {
        _gems = 0;
        _highScore = 0;
        _playerName = "New Player";
        _highestBlockIndex = 0;
    }
}
