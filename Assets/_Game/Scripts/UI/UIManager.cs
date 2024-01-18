using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private CanvasGroup _menuUI;
    [SerializeField] private CanvasGroup _topUI;
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
        _playUI.gameObject.SetActive(false);
    }
    public void OnPlayState()
    {
        ChangeUI(_playUI, true);
        _menuUI.gameObject.SetActive(false);
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
