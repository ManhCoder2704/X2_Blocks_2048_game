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
    [SerializeField] private Button _profileBtn;
    [SerializeField] private Button _settingBtn;

    public Button currentBtn;
    void Start()
    {
        currentBtn = _homeBtn;
        _homeBtn.onClick.AddListener(OnHome);
        _shopBtn.onClick.AddListener(OnShop);
        _rankBtn.onClick.AddListener(OnRank);
        _profileBtn.onClick.AddListener(OnProfile);
        _settingBtn.onClick.AddListener(OnSetting);
    }

    private void OnSetting()
    {
        SwapButton(_settingBtn);
        UIManager.Instance.OnSettingState();
    }

    private void OnProfile()
    {
        SwapButton(_profileBtn);
        UIManager.Instance.OnProfileState();
    }

    private void OnRank()
    {
        SwapButton(_rankBtn);
        UIManager.Instance.OnRankState();
    }

    private void OnShop()
    {
        SwapButton(_shopBtn);
        UIManager.Instance.OnShopState();
    }

    private void OnHome()
    {
        SwapButton(_homeBtn);
        UIManager.Instance.OnHomeState();
    }
    public void SwapButton(Button btn)
    {
        currentBtn.interactable = true;
        btn.interactable = false;
        currentBtn = btn;
    }
}
