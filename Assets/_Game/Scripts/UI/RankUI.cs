using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : Singleton<RankUI>
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private Transform _toggleBtn;
    [SerializeField] private Button _escapeButton;

    private float _duration = 0.5f;

    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(GameplayManager.Instance.CurrentState == GameStateEnum.Playing);
    }
    void Start()
    {
        _switchBtn.onClick.AddListener(Switch);
        _escapeButton.onClick.AddListener(CloseRank);
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

    private void CloseRank()
    {
        UIManager.Instance.OnPlayState();
    }
}
