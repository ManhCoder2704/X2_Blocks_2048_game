using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : Singleton<ShopUI>
{
    [SerializeField] private Button _buyBtn;
    [SerializeField] private Button _purchaseBtn;
    void Awake()
    {
        _buyBtn.onClick.AddListener(BuyDiamond);
        _purchaseBtn.onClick.AddListener(BuyDiamond);
    }

    private void BuyDiamond()
    {
        throw new NotImplementedException();
    }
}
