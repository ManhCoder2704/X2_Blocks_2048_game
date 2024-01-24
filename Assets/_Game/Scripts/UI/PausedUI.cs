using System;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : Singleton<PausedUI>
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _vibraBtn;
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Button _themeBtn;
    void Awake()
    {
        _homeBtn.onClick.AddListener(OnHome);
        _continueBtn.onClick.AddListener(Continue);
        _restartBtn.onClick.AddListener(Restart);
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
        _themeBtn.onClick.AddListener(OnTheme);
    }

    private void OnTheme()
    {
        UIManager.Instance.OnThemeState();
    }

    private void OnMusic()
    {
        throw new NotImplementedException();
    }

    private void OnVibration()
    {
        throw new NotImplementedException();
    }
    public void Restart()
    {
        GameplayManager.Instance.ResetBoard();
        UIManager.Instance.OnPlayState();
    }

    private void Continue()
    {
        UIManager.Instance.OnPlayState();
    }

    private void OnHome()
    {
        UIManager.Instance.OnHomeState();
    }
}
