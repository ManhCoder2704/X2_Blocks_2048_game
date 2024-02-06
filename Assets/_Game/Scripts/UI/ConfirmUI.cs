using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUI : UIBase
{
    [SerializeField] private TMP_Text _contentTxt;
    [SerializeField] private TMP_Text _titleTxt;
    [SerializeField] private Button _escapeBtn;
    [SerializeField] private Button _escapePanel;
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _noBtn;

    private Action OnAgree;
    private Action OnDisagree;
    private void Start()
    {
        _yesBtn.onClick.AddListener(Agree);
        _noBtn.onClick.AddListener(Disagree);
        _escapeBtn.onClick.AddListener(Disagree);
        _escapePanel.onClick.AddListener(Disagree);
    }

    public void OnInit(Action agreeCallBack, Action disagreeCallBack, string content, string title)
    {
        _contentTxt.text = content;
        OnAgree = agreeCallBack;
        OnDisagree = disagreeCallBack;
        _titleTxt.text = title;
    }
    private void Disagree()
    {
        OnDisagree?.Invoke();
        this.gameObject.SetActive(false);
    }

    private void Agree()
    {
        OnAgree?.Invoke();
        this.gameObject.SetActive(false);
    }
}
