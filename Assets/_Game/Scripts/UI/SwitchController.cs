using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private Image _switchBG;
    [SerializeField] private Transform _toggleBtn;

    private bool _isOn = true;
    private float _duration = 0.5f;

    void Start()
    {
        if (_isOn)
        {
            _switchBG.color = Color.green;
        }
        _switchBtn.onClick.AddListener(Switch);
    }

    private void Switch()
    {
        _switchBtn.interactable = false;
        _toggleBtn.DOLocalMoveX(-_toggleBtn.localPosition.x, _duration)
            .OnComplete(() =>
            {
                SetColor();
                _switchBtn.interactable = true;
            });
    }
    private void SetColor()
    {
        if (_isOn)
        {
            _switchBG.DOColor(Color.gray, _duration);
        }
        else
        {
            _switchBG.DOColor(Color.green, _duration);
        }
        _isOn = !_isOn;
    }
}
