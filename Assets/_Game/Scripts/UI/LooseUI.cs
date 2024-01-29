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
        _continueBtn.onClick.AddListener(Continue);
        _restartBtn.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        GameplayManager.Instance.ResetBoard();
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }

    private void Continue()
    {
        //ToDo : Get Skill
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }
}
