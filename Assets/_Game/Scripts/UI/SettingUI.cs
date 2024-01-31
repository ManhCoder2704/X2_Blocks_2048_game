using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIBase
{
    [SerializeField] private Button _vibraBtn;
    [SerializeField] private Button _musicBtn;
    [SerializeField] private IconController _musicIcon;
    [SerializeField] private IconController _vibraIcon;

    void Awake()
    {
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
    }
    private void OnEnable()
    {
        _musicIcon.Swap(RuntimeDataManager.Instance.SettingData.IsSoundOn);
        _vibraIcon.Swap(RuntimeDataManager.Instance.SettingData.IsVibrationOn);
    }

    private void OnMusic()
    {
        _musicIcon.Swap(RuntimeDataManager.Instance.SettingData.IsSoundOn);
        SoundManager.Instance.ChangeSoundable();
    }

    private void OnVibration()
    {
        _vibraIcon.Swap(RuntimeDataManager.Instance.SettingData.IsVibrationOn);
        SoundManager.Instance.ChangeVibratable();
    }
}
