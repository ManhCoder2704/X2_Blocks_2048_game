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
        OnGemChange(RuntimeDataManager.Instance.PlayerData.Gems);
        RuntimeDataManager.Instance.PlayerData.OnGemsChange += OnGemChange;
    }
    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(_isPopup);
    }
    private void OnGemChange(int gems)
    {
        _gemsCountText.LerpNumber(gems);
    }
    private void CloseShop()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
