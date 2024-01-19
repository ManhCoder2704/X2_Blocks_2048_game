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
    [SerializeField] private CanvasGroup _profileUI;
    [SerializeField] private CanvasGroup _settingUI;

    private CanvasGroup _currentUI;

    void Start()
    {
        _currentUI = _homeUI;
        OnHomeState();
    }

    public void OnHomeState()
    {
        _homeUI.gameObject.SetActive(true);
        _homeUI.alpha = 1.0f;
        _homeUI.interactable = true;
        _menuUI.gameObject.SetActive(true);
        _playUI.gameObject.SetActive(false);
        _pauseUI.gameObject.SetActive(false);
        _shopUI.gameObject.SetActive(false);
        _rankUI.gameObject.SetActive(false);
        _profileUI.gameObject.SetActive(false);
        _settingUI.gameObject.SetActive(false);
    }

    public void OnPlayState()
    {
        ChangeUI(_playUI, true);
        _menuUI.gameObject.SetActive(false);
    }
    public void OnPausedState()
    {
        ChangeUI(_pauseUI, false);
    }
    public void OnShopState()
    {
        ChangeUI(_shopUI, false);
    }
    public void OnRankState()
    {
        ChangeUI(_rankUI, false);
    }
    public void OnProfileState()
    {
        ChangeUI(_profileUI, false);
    }
    public void OnSettingState()
    {
        ChangeUI(_settingUI, false);
    }

    private void ChangeUI(CanvasGroup on, bool deactive)
    {
        CanvasGroup temp = _currentUI;
        on.interactable = true;
        on.alpha = 1.0f;
        on.gameObject.SetActive(true);
        _currentUI = on;
        temp.interactable = false;
        temp.alpha = 0f;
        if (!deactive) return;
        temp.gameObject.SetActive(false);
    }
}
