using System;
using UnityEngine;
using UnityEngine.UI;

public class LooseUI : UIBase
{
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
    }

    private void Restart()
    {
        GameplayManager.Instance.ResetBoard();
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }

    private void Revive()
    {
        //ToDo : Get Skill
        RuntimeDataManager.Instance.PlayerData.Gems -= 100;
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }
}
