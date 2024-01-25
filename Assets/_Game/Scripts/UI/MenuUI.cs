using UnityEngine;
using UnityEngine.UI;

public class MenuUI : UIBase
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
        UIManager.Instance.OpenUIOrPopup(UIType.SettingUI);
    }

    private void OnTheme()
    {
        TurnOn(_themeBtn);
        UIManager.Instance.OpenUIOrPopup(UIType.ThemeUI);
    }

    private void OnRank()
    {
        TurnOn(_rankBtn);
        UIManager.Instance.OpenUIOrPopup(UIType.RankUI);
    }

    private void OnShop()
    {
        TurnOn(_shopBtn);
        UIManager.Instance.OpenUIOrPopup(UIType.ShopUI);
    }

    private void OnHome()
    {
        TurnOn(_homeBtn);
        UIManager.Instance.OpenUIOrPopup(UIType.HomeUI);
    }
    private void TurnOn(Button off)
    {
        off.enabled = false;
        _currentBtn.enabled = true;
        _currentBtn = off;
    }
}
