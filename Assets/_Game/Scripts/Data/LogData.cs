using System;

[Serializable]
public class LogData
{
    private DateTime _lastLoginTime;
    private int _playTime; // hours

    public DateTime LastLoginTime { get => _lastLoginTime; set => _lastLoginTime = value; }
    public int PlayTime { get => _playTime; set => _playTime = value; }
}
