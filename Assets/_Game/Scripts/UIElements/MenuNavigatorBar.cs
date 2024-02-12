using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuNavigatorBar : MonoBehaviour
{
    [SerializeField] private Button _rankBtn;
    [SerializeField] private Button _shopBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _themeBtn;
    [SerializeField] private Button _lobbyBtn;

    [SerializeField] private GameObject _rankIcon;
    [SerializeField] private GameObject _shopIcon;
    [SerializeField] private GameObject _settingsIcon;
    [SerializeField] private GameObject _themeIcon;
    [SerializeField] private GameObject _lobbyIcon;

    [SerializeField] private GameObject _focusBG;
    [SerializeField] private TMP_Text _focusText;

    private GameObject _currentIcon;
    private bool _isAnimating;

    private void Start()
    {
        _currentIcon = _lobbyIcon;
        _rankBtn.onClick.AddListener(OpenRank);
        _shopBtn.onClick.AddListener(OpenShop);
        _settingsBtn.onClick.AddListener(() => OnClick("Setting", _settingsIcon, _settingsBtn.transform, UIType.SettingUI));
        _themeBtn.onClick.AddListener(() => OnClick("Theme", _themeIcon, _themeBtn.transform, UIType.ThemeUI));
        _lobbyBtn.onClick.AddListener(() => OnClick("Lobby", _lobbyIcon, _lobbyBtn.transform, UIType.HomeUI));
    }

    private void OnClick(string text, GameObject icon, Transform btn, UIType uiType)
    {
        if (_isAnimating) return;
        _isAnimating = true;

        SoundManager.Instance.PlaySFX(SFXType.Click);
        UIManager.Instance.OpenUI(uiType);

        Sequence sequence = DOTween.Sequence();
        sequence.OnStart(() =>
        {
            _focusText.DOFade(0f, 0.1f)
            .onComplete += () => _focusText.text = text;
        });
        sequence.Append(_currentIcon.transform.DOLocalMoveY(11f, 0.1f));
        sequence.Append(_focusBG.transform.DOMoveX(btn.position.x, 0.3f).SetEase(Ease.InFlash));
        sequence.Append(icon.transform.DOLocalMoveY(30f, 0.1f));
        sequence.onComplete += () =>
        {
            _focusText.DOFade(1f, 0.1f);
            _currentIcon = icon;
            _isAnimating = false;
        };
    }

    public void OpenShop()
    {
        OnClick("Shop", _shopIcon, _shopBtn.transform, UIType.ShopUI);
    }

    public void OpenRank()
    {
        OnClick("Rank", _rankIcon, _rankBtn.transform, UIType.RankUI);
    }
}
