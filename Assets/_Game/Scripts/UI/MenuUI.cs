using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : Singleton<MenuUI>
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _shopBtn;
    [SerializeField] private Button _rankBtn;
    [SerializeField] private Button _themeBtn;
    [SerializeField] private Button _settingBtn;

    void Awake()
    {
        _homeBtn.Select();
        _homeBtn.onClick.AddListener(OnHome);
        _shopBtn.onClick.AddListener(OnShop);
        _rankBtn.onClick.AddListener(OnRank);
        _themeBtn.onClick.AddListener(OnTheme);
        _settingBtn.onClick.AddListener(OnSetting);
    }

    private void OnSetting()
    {
        UIManager.Instance.OnSettingState();
    }

    private void OnTheme()
    {
        UIManager.Instance.OnThemeState();
    }

    private void OnRank()
    {
        UIManager.Instance.OnRankState();
    }

    private void OnShop()
    {
        UIManager.Instance.OnShopState();
    }

    private void OnHome()
    {
        UIManager.Instance.OnHomeState();
    }

}
