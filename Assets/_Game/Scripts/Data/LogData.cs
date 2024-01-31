using System;
using UnityEngine;

[Serializable]
public class LogData
{
    [SerializeField] private DateTime _lastLoginTime;
    [SerializeField] private int _playTime; // hours

    public DateTime LastLoginTime { get => _lastLoginTime; set => _lastLoginTime = value; }
    public int PlayTime { get => _playTime; set => _playTime = value; }

    public LogData()
    {
        _lastLoginTime = DateTime.Now;
        _playTime = 0;
    }
}
