using UnityEngine;
using UnityEngine.UI;

public class MenuUI : Singleton<MenuUI>
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _shopBtn;
    [SerializeField] private Button _rankBtn;
    [SerializeField] private Button _themeBtn;
    [SerializeField] private Button _settingBtn;

    private Button _currentBtn;
    void Awake()
    {
        _homeBtn.Select();
        _currentBtn = _homeBtn;
        _homeBtn.onClick.AddListener(OnHome);
        _shopBtn.onClick.AddListener(OnShop);
        _rankBtn.onClick.AddListener(OnRank);
        _themeBtn.onClick.AddListener(OnTheme);
        _settingBtn.onClick.AddListener(OnSetting);
    }

    private void OnSetting()
    {
        TurnOn(_settingBtn);
        UIManager.Instance.OnSettingState();
    }

    private void OnTheme()
    {
        TurnOn(_themeBtn);
        UIManager.Instance.OnThemeState();
    }

    private void OnRank()
    {
        TurnOn(_rankBtn);
        UIManager.Instance.OnRankState();
    }

    private void OnShop()
    {
        TurnOn(_shopBtn);
        UIManager.Instance.OnShopState();
    }

    private void OnHome()
    {
        TurnOn(_homeBtn);
        UIManager.Instance.OnHomeState();
    }
    private void TurnOn(Button off)
    {
        off.enabled = false;
        _currentBtn.enabled = true;
        _currentBtn = off;
    }
}
