using System;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _vibraBtn;
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Button _themeBtn;
    void Awake()
    {
        _homeBtn.onClick.AddListener(() => UIManager.Instance.OnHomeState());
        _continueBtn.onClick.AddListener(() => UIManager.Instance.OnPlayState());
        _restartBtn.onClick.AddListener(() => UIManager.Instance.Restart());
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
        _themeBtn.onClick.AddListener(() => UIManager.Instance.OnThemeState());
    }

    private void OnMusic()
    {
        //ToDo
    }

    private void OnVibration()
    {
        //ToDo
    }

}
