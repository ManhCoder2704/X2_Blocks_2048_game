using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    [SerializeField] private Button _buyBtn;
    [SerializeField] private Button _purchaseBtn;
    [SerializeField] private Button _escapeButton;
    void Awake()
    {
        _buyBtn.onClick.AddListener(BuyDiamond);
        _purchaseBtn.onClick.AddListener(BuyDiamond);
        _escapeButton.onClick.AddListener(CloseShop);
    }
    void OnEnable()
    {
        if (GameplayManager.Instance.CurrentState == GameStateEnum.Pause)
        {
            _isPopup = true;
            _escapeButton.gameObject.SetActive(true);
        }
        else
        {
            _isPopup = false;
            _escapeButton.gameObject.SetActive(false);
        }
    }

    private void BuyDiamond()
    {
        throw new NotImplementedException();
    }

    private void CloseShop()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
