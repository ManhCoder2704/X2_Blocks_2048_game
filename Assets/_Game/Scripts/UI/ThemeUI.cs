using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ThemeUI : UIBase
{
    [SerializeField] private Button _escapeButton;

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
