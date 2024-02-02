using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUI : UIBase
{
    [SerializeField] private TMP_Text _contentTxt;
    [SerializeField] private Button _escapeBtn;
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _noBtn;

    private Action OnAgree;
    private Action OnDisagree;
    private void Start()
    {
        _yesBtn.onClick.AddListener(Agree);
        _noBtn.onClick.AddListener(Disagree);
        _escapeBtn.onClick.AddListener(Disagree);
    }

    public void OnInit(Action agreeCallBack, Action disagreeCallBack, string content)
    {
        _contentTxt.text = content;
        OnAgree = agreeCallBack;
        OnDisagree = disagreeCallBack;
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
