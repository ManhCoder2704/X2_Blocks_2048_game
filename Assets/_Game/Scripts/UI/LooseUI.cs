using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LooseUI : Singleton<LooseUI>
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _restartBtn;

    void Start()
    {
        _homeBtn.onClick.AddListener(() => UIManager.Instance.OnHomeState());
        _restartBtn.onClick.AddListener(() => PausedUI.Instance.Restart());
    }
}
