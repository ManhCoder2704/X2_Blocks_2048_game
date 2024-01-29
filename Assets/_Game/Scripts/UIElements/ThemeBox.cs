using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeBox : MonoBehaviour
{
    [SerializeField] private Button _themeBtn;
    [SerializeField] private GameObject _status;
    [SerializeField] private Image _background;
    [SerializeField] private TMPro.TMP_Text _price;

    private void Start()
    {
        _themeBtn.onClick.AddListener(ChooseTheme);
    }

    private void ChooseTheme()
    {

    }
}
