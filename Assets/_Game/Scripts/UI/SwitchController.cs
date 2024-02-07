using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private GameObject _fill;
    [SerializeField] private Transform _toggle;
    [SerializeField] private Vector2 _onPos;

    private bool _isOn;
    private float _duration = 0.5f;
    void Start()
    {
        _switchBtn.onClick.AddListener(Switch);
    }

    private void Switch()
    {
        _switchBtn.interactable = false;
        _toggle.DOLocalMoveX(-_toggle.localPosition.x, _duration)
            .OnComplete(() =>
            {
                SetColor(_isOn);
                _switchBtn.interactable = true;
            });
    }
    public void SetColor(bool isOn)
    {
        if (isOn)
        {
            _fill.SetActive(true);
            _toggle.DOLocalMoveX(_onPos.x, 0f);
        }
        else
        {
            _fill.SetActive(false);
            _toggle.DOLocalMoveX(-_onPos.x, 0f);
        }
        _isOn = !isOn;
    }
}
