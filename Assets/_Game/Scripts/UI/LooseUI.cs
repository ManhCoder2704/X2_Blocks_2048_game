using UnityEngine;
using UnityEngine.UI;

public class LooseUI : UIBase
{
    //[SerializeField] private Button _escapeBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;

    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX(SFXType.Lost);
    }
    void Start()
    {
        _continueBtn.onClick.AddListener(Revive);
        _restartBtn.onClick.AddListener(Restart);
        //_escapeBtn.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        GameplayManager.Instance.ResetBoard();
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }

    private void Revive()
    {
        GameplayManager.Instance.Board.RemoveTwoColumn();

        RuntimeDataManager.Instance.PlayerData.Gems -= 700;
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }
}
