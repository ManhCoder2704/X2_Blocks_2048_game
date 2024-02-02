using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : UIBase
{
    [SerializeField] private TMP_Text _contentTxt;
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _escapeBtn;

    private void Start()
    {
        _yesBtn.onClick.AddListener(Agree);
        _escapeBtn.onClick.AddListener(Agree);
    }

    private void Agree()
    {
        this.gameObject.SetActive(false);
    }
    public void SetContent(string content)
    {
        _contentTxt.text = content;
    }
}
