using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private Transform _toggleBtn;

    private bool _isSelected;
    private float _duration = 0.5f;

    void Start()
    {
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
        if (_isSelected)
        {
            _switchBtn.image.color = Color.black;
        }
        else
        {
            _switchBtn.image.color = Color.green;
        }
        _isSelected = !_isSelected;
    }
}
