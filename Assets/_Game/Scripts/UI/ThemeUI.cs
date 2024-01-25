using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ThemeUI : UIBase
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private Transform _toggleBtn;
    [SerializeField] private Button _escapeButton;

    private float _duration = 0.5f;
    void OnEnable()
    {
        if (GameplayManager.Instance.CurrentState == GameStateEnum.Pause)
        {
            _isPopup = true;
            _escapeButton.gameObject.SetActive(true);
        }
        else
        {
            _isPopup = false;
            _escapeButton.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        _switchBtn.onClick.AddListener(Switch);
        _escapeButton.onClick.AddListener(CloseTheme);
    }
    private void Switch()
    {
        _switchBtn.interactable = false;
        _toggleBtn.DOLocalMoveX(-_toggleBtn.localPosition.x, _duration)
            .OnComplete(() =>
            {
                _switchBtn.interactable = true;
            });
    }

    private void CloseTheme()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
