using UnityEngine;
using UnityEngine.UI;

public class LooseUI : UIBase
{
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;

    void Start()
    {
        /*_continueBtn.onClick.AddListener(() => UIManager.Instance.OnPlayState());
        _restartBtn.onClick.AddListener(() => UIManager.Instance.Restart());*/
    }
}
