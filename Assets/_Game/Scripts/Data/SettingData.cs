using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingData
{
    [SerializeField] private bool _isSoundOn;
    [SerializeField] private bool _isVibrationOn;
    [SerializeField] private bool _isRemoveAds;
    [SerializeField] private int _themeIndex;
    [SerializeField] private List<bool> _ownThemeStatusList;

    public bool IsSoundOn { get => _isSoundOn; set => _isSoundOn = value; }
    public bool IsVibrationOn { get => _isVibrationOn; set => _isVibrationOn = value; }
    public bool IsRemoveAds { get => _isRemoveAds; set => _isRemoveAds = value; }
    public int ThemeIndex { get => _themeIndex; set => _themeIndex = value; }
    public List<bool> OwnThemeStatusList { get => _ownThemeStatusList; set => _ownThemeStatusList = value; }

    public SettingData(int length)
    {
        _isSoundOn = true;
        _isVibrationOn = true;
        _isRemoveAds = false;
        _themeIndex = 0;
        _ownThemeStatusList = new List<bool>();
        for (int i = 0; i < length; i++)
        {
            _ownThemeStatusList.Add(false);
        }
        _ownThemeStatusList[0] = true;
        _ownThemeStatusList[1] = true;
    }
}
