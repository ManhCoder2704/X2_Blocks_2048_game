using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIBase
{
    [SerializeField] private Button _vibraBtn;
    [SerializeField] private Button _musicBtn;

    void Awake()
    {
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
    }

    private void OnMusic()
    {
        SoundManager.Instance.ChangeSoundable();
    }

    private void OnVibration()
    {
        SoundManager.Instance.ChangeVibratable();
    }
}
