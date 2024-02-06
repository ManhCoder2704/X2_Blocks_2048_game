using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : UIBase
{
    [SerializeField] private TMP_Text _contentTxt;
    [SerializeField] private TMP_Text _titleTxt;
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _escapeBtn;
    [SerializeField] private Button _escapePanel;

    private void Start()
    {
        _yesBtn.onClick.AddListener(Agree);
        _escapeBtn.onClick.AddListener(Agree);
        _escapePanel.onClick.AddListener(Agree);
    }

    private void Agree()
    {
        this.gameObject.SetActive(false);
    }
    public void SetContent(string content, string title)
    {
        _contentTxt.text = content;
        _titleTxt.text = title;
    }
}
