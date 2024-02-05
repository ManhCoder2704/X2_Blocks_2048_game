using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClipList;
    private bool soundAble = true;
    private bool vibratAble = true;

    public void OnInit()
    {
        soundAble = RuntimeDataManager.Instance.SettingData.IsSoundOn;
        vibratAble = RuntimeDataManager.Instance.SettingData.IsVibrationOn;
    }
    public void PlaySFX(SFXType sfxType)
    {
        if (!soundAble) return;
        _audioSource.PlayOneShot(_audioClipList[(int)sfxType]);
    }
    public void VibrateDevice()
    {
        if (!vibratAble) return;
        Handheld.Vibrate();
    }
    public void ChangeSoundable()
    {
        soundAble = !soundAble;
        RuntimeDataManager.Instance.SettingData.IsSoundOn = soundAble;
    }
    public void ChangeVibratable()
    {
        vibratAble = !vibratAble;
        RuntimeDataManager.Instance.SettingData.IsVibrationOn = vibratAble;
    }
}

public enum SFXType
{
    Start = 0,
    Click = 1,
    Close = 2,
    Shoot = 3,
    Merge = 4,
    Combo = 5,
    Skill = 6,
    Lost = 7
}