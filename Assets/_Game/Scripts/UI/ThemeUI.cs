using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ThemeUI : UIBase
{
    [SerializeField] private BackGroundSO _bgSO;
    [SerializeField] private Button _escapeButton;
    [SerializeField] private ThemeBox _prefabThemeBox;
    [SerializeField] private Transform _themeBoxContainer;

    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(_isPopup);
    }
    private void Start()
    {
        _escapeButton.onClick.AddListener(CloseTheme);
    }
    private void CloseTheme()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
