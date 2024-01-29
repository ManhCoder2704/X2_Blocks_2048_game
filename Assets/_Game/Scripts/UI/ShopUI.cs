using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    [SerializeField] private TMP_Text _gemsCountText;
    [SerializeField] private Button _escapeButton;
    void Awake()
    {

        _escapeButton.onClick.AddListener(CloseShop);
    }
    void OnEnable()
    {
        _gemsCountText.LerpNumber(RuntimeDataManager.Instance.PlayerData.Gems);
        _escapeButton.gameObject.SetActive(_isPopup);
    }

    private void CloseShop()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
