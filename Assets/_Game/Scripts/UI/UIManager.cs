using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private CanvasGroup _menuUI;
    [SerializeField] private CanvasGroup _homeUI;
    [SerializeField] private CanvasGroup _playUI;
    [SerializeField] private CanvasGroup _pauseUI;
    [SerializeField] private CanvasGroup _shopUI;
    [SerializeField] private CanvasGroup _rankUI;
    [SerializeField] private CanvasGroup _settingUI;
    [SerializeField] private CanvasGroup _looseUI;
    [SerializeField] private CanvasGroup _themeUI;
    [SerializeField] private CanvasGroup _confirmRestartUI;
    [SerializeField] private CanvasGroup _tutorialUI;
    [SerializeField] private Image _background;

    private CanvasGroup _currentUI;

    void Awake()
    {
        _currentUI = _homeUI;
        _homeUI.interactable = true;
        _homeUI.alpha = 1.0f;
        _homeUI.gameObject.SetActive(true);
        _playUI.gameObject.SetActive(false);
        _pauseUI.gameObject.SetActive(false);
        _shopUI.gameObject.SetActive(false);
        _rankUI.gameObject.SetActive(false);
        _settingUI.gameObject.SetActive(false);
        _looseUI.gameObject.SetActive(false);
        _themeUI.gameObject.SetActive(false);
        _confirmRestartUI.gameObject.SetActive(false);
        _tutorialUI.gameObject.SetActive(false);
    }
    public void OnHomeState()
    {
        GameplayManager.Instance.ChangeGameState(GameStateEnum.Prepare);
        ChangeUI(_homeUI);
        _menuUI.gameObject.SetActive(true);
        _background.gameObject.SetActive(true);
    }
    public void OnPlayState()
    {
        GameplayManager.Instance.ChangeGameState(GameStateEnum.Playing);
        ChangeUI(_playUI);
        _menuUI.gameObject.SetActive(false);
        _background.gameObject.SetActive(false);
    }
    public void OnPausedState()
    {
        GameplayManager.Instance.ChangeGameState(GameStateEnum.Pause);
        ChangeUI(_pauseUI);
    }
    public void OnShopState()
    {
        ChangeUI(_shopUI);
    }
    public void OnRankState()
    {
        ChangeUI(_rankUI);
    }
    public void OnSettingState()
    {
        ChangeUI(_settingUI);
    }
    public void OnThemeState()
    {
        ChangeUI(_themeUI);
    }
    public void OnLooseState()
    {
        GameplayManager.Instance.ChangeGameState(GameStateEnum.Loose);
        ChangeUI(_looseUI);
    }
    public void OnTutorialState()
    {
        GameplayManager.Instance.ChangeGameState(GameStateEnum.Turtorial);
        ChangeUI(_tutorialUI);
    }
    public void Restart()
    {
        GameplayManager.Instance.ResetBoard();
        OnPlayState();
    }
    private void ChangeUI(CanvasGroup on)
    {
        if (_currentUI == on) return;
        on.interactable = true;
        on.alpha = 1.0f;
        on.gameObject.SetActive(true);
        CanvasGroup temp = _currentUI;
        _currentUI = on;
        temp.interactable = false;
        temp.alpha = 0f;
        temp.gameObject.SetActive(false);
    }


}
