using UnityEngine;
using UnityEngine.UI;

public class PausedUI : UIBase
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _vibraBtn;
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Button _themeBtn;
    void Awake()
    {
        _homeBtn.onClick.AddListener(() => UIManager.Instance.OpenUI(UIType.HomeUI));
        _continueBtn.onClick.AddListener(() => UIManager.Instance.ClosePopup(this));
        _restartBtn.onClick.AddListener(() =>
        {
            GameplayManager.Instance.ResetBoard();
            UIManager.Instance.OpenUI(UIType.PlayUI);
        });
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
        _themeBtn.onClick.AddListener(() => UIManager.Instance.OpenUI(UIType.ThemePopupUI));
    }

    private void OnMusic()
    {
        SoundManager.Instance.ChangeSoundable();
    }

    private void OnVibration()
    {
        SoundManager.Instance.ChangeVibratable();
    }

}
