using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIBase
{
    [SerializeField] private Button _vibraBtn;
    [SerializeField] private Button _musicBtn;
    //[SerializeField] private Button _resetDataBtn;
    [SerializeField] private IconController _musicIcon;
    [SerializeField] private IconController _vibraIcon;

    void Awake()
    {
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
        //_resetDataBtn.onClick.AddListener(ResetData);
    }

    private void ResetData()
    {
        RuntimeDataManager.Instance.ResetData();
    }

    private void OnEnable()
    {
        _musicIcon.Swap(RuntimeDataManager.Instance.SettingData.IsSoundOn);
        _vibraIcon.Swap(RuntimeDataManager.Instance.SettingData.IsVibrationOn);
    }

    private void OnMusic()
    {
        SoundManager.Instance.ChangeSoundable();
        _musicIcon.Swap(RuntimeDataManager.Instance.SettingData.IsSoundOn);
    }

    private void OnVibration()
    {
        SoundManager.Instance.ChangeVibratable();
        _vibraIcon.Swap(RuntimeDataManager.Instance.SettingData.IsVibrationOn);
    }
}
