using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LooseUI : Singleton<LooseUI>
{
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;

    void Start()
    {
        _continueBtn.onClick.AddListener(() => UIManager.Instance.OnPlayState());
        _restartBtn.onClick.AddListener(() => PausedUI.Instance.Restart());
    }
}
