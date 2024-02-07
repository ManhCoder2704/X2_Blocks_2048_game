using System;
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
    [SerializeField] private Button _escapeBtn;

    private SwitchController _musicSwitcher;
    private SwitchController _vibraSwitcher;
    void Awake()
    {
        _homeBtn.onClick.AddListener(() => UIManager.Instance.OpenUI(UIType.HomeUI));
        _continueBtn.onClick.AddListener(Continue);
        _restartBtn.onClick.AddListener(ConfirmRestart);
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
        _themeBtn.onClick.AddListener(() => UIManager.Instance.OpenUI(UIType.ThemePopupUI));
        _escapeBtn.onClick.AddListener(Continue);

        _musicSwitcher = _musicBtn.GetComponent<SwitchController>();
        _vibraSwitcher = _vibraBtn.GetComponent<SwitchController>();
    }
    private void OnEnable()
    {
        _musicSwitcher.SetColor(RuntimeDataManager.Instance.SettingData.IsSoundOn);
        _vibraSwitcher.SetColor(RuntimeDataManager.Instance.SettingData.IsVibrationOn);
    }
    private void Continue()
    {
        UIManager.Instance.ClosePopup(this);
    }

    private void ConfirmRestart()
    {
        Action agree = Restart;
        agree += Continue;
        Action disagree = () => this.gameObject.SetActive(true);
        UIManager.Instance.OpenConfirmUI(agree, disagree, "Do You Want To Restart?", "Restart", () => this.gameObject.SetActive(false));
    }
    private void Restart()
    {
        GameplayManager.Instance.ResetBoard();
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
