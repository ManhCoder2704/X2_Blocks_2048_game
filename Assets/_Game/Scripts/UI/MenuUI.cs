using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _shopBtn;
    [SerializeField] private Button _rankBtn;
    [SerializeField] private Button _profileBtn;
    [SerializeField] private Button _settingBtn;

    void Start()
    {
        _homeBtn.onClick.AddListener(OnHome);
        _shopBtn.onClick.AddListener(OnShop);
        _rankBtn.onClick.AddListener(OnRank);
        _profileBtn.onClick.AddListener(OnProfile);
        _settingBtn.onClick.AddListener(OnSetting);
    }

    private void OnSetting()
    {
        throw new NotImplementedException();
    }

    private void OnProfile()
    {
        throw new NotImplementedException();
    }

    private void OnRank()
    {
        throw new NotImplementedException();
    }

    private void OnShop()
    {
        throw new NotImplementedException();
    }

    private void OnHome()
    {
        UIManager.Instance.OnHomeState();
    }
}
