using System;

[Serializable]
public class SettingData
{
    private bool _isSoundOn;
    private bool _isVibrationOn;
    private bool _isRemoveAds;
    private int _themeIndex;

    public bool IsSoundOn { get => _isSoundOn; set => _isSoundOn = value; }
    public bool IsVibrationOn { get => _isVibrationOn; set => _isVibrationOn = value; }
    public bool IsRemoveAds { get => _isRemoveAds; set => _isRemoveAds = value; }
    public int ThemeIndex { get => _themeIndex; set => _themeIndex = value; }
}
