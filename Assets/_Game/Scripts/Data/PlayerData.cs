using System;

[Serializable]
public class PlayerData
{
    private int _gems;
    private int _highScore;
    private string _playerName;
    private int _numberOfGame;
    private int _highestBlockIndex;
    private int _level;

    public int Gems { get => _gems; set => _gems = value; }
    public int HighScore { get => _highScore; set => _highScore = value; }
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public int NumberOfGame { get => _numberOfGame; set => _numberOfGame = value; }
    public int HighestBlockIndex { get => _highestBlockIndex; set => _highestBlockIndex = value; }
    public int Level { get => _level; set => _level = value; }
}
