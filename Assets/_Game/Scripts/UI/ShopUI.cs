using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIBase
{

    [SerializeField] private Button _escapeButton;
    void Awake()
    {

        _escapeButton.onClick.AddListener(CloseShop);
    }
    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(_isPopup);
    }

    private void CloseShop()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
