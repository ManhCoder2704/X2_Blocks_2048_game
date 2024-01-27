using System;
using UnityEngine;

[Serializable]
public class SettingData
{
    [SerializeField] private bool _isSoundOn;
    [SerializeField] private bool _isVibrationOn;
    [SerializeField] private bool _isRemoveAds;
    [SerializeField] private int _themeIndex;

    public bool IsSoundOn { get => _isSoundOn; set => _isSoundOn = value; }
    public bool IsVibrationOn { get => _isVibrationOn; set => _isVibrationOn = value; }
    public bool IsRemoveAds { get => _isRemoveAds; set => _isRemoveAds = value; }
    public int ThemeIndex { get => _themeIndex; set => _themeIndex = value; }

    public SettingData()
    {
        _isSoundOn = true;
        _isVibrationOn = true;
        _isRemoveAds = false;
        _themeIndex = 0;
    }
}
