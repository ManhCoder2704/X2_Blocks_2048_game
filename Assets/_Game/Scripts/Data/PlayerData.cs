using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private int _gems;
    [SerializeField] private string _highScore;
    [SerializeField] private string _playerName;
    [SerializeField] private int _highestBlockIndex;

    public int Gems
    {
        get => _gems;
        set
        {
            _gems = value;
            OnGemsChange?.Invoke(value);
        }
    }
    public string HighScore
    {
        get => _highScore;
        set
        {
            _highScore = value;
            OnHighScoreChange?.Invoke(value);
        }
    }
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public int HighestBlockIndex { get => _highestBlockIndex; set => _highestBlockIndex = value; }
    public Action<int> OnGemsChange;
    public Action<string> OnHighScoreChange;

    public PlayerData()
    {
        _gems = 200;
        _highScore = "0";
        _playerName = "New Player";
        _highestBlockIndex = 0;
    }
}
