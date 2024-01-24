using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
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
        _escapeButton.gameObject.SetActive(GameplayManager.Instance.CurrentState == GameStateEnum.Playing);
    }

    private void BuyDiamond()
    {
        throw new NotImplementedException();
    }

    private void CloseShop()
    {
        UIManager.Instance.OnPlayState();
    }
}
