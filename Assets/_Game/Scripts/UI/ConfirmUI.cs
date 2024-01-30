using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUI : UIBase
{
    [SerializeField] private TMP_Text _contentTxt;
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _noBtn;

    private void Start()
    {
        _yesBtn.onClick.AddListener(Agree);
        _noBtn.onClick.AddListener(Disagree);
    }

    private void Disagree()
    {
        throw new NotImplementedException();
    }

    private void Agree()
    {
        throw new NotImplementedException();
    }
    private void SetContent(string content)
    {
        _contentTxt.text = content;
    }
}
