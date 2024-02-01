using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private Image _switchBG;
    [SerializeField] private Transform _toggleBtn;
    [SerializeField] private Vector2 _onPos;

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
        if (_switchBG.color == Color.green)
        {
            _switchBG.DOColor(Color.gray, _duration);
        }
        else
        {
            _switchBG.DOColor(Color.green, _duration);
        }
    }
    public void CheckStatus(bool isOn)
    {
        if (isOn)
        {
            _switchBG.color = Color.green;
            _toggleBtn.DOLocalMoveX(_onPos.x,0f);
        }
        else
        {
            _switchBG.color = Color.gray;
            _toggleBtn.DOLocalMoveX(-_onPos.x, 0f);

        }
    }
}
