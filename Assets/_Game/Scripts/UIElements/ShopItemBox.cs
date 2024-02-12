using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemBox : MonoBehaviour
{
    [SerializeField] private Button _shopItemBtn;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _priceTxt;
    [SerializeField] private TMP_Text _bonusTxt;
    [SerializeField] private TMP_Text _gemCount;
    [SerializeField] private TMP_Text _packageName;
    [SerializeField] private GameObject _badge;

    private int _gemsCount;
    // Start is called before the first frame update
    void Start()
    {
        _shopItemBtn.onClick.AddListener(ConfirmPurchase);
    }

    public void OnInit(ShopItemData itemData)
    {
        _background.sprite = itemData.Image;
        float bonus = itemData.Bonus;
        if (bonus <= 0)
        {
            _bonusTxt.enabled = false;
            _badge.SetActive(false);
        }
        _bonusTxt.text = $"+{bonus}% BONUS";
        CultureInfo culture = new CultureInfo("vi-VN");
        _priceTxt.text = itemData.Price.ToString("C", culture);
        _gemsCount = Mathf.FloorToInt(itemData.GemsCount * (1 + (bonus / 100f)));
        _gemCount.text = _gemsCount.ToString();
        _packageName.text = itemData.PackageName;
    }
    private void ConfirmPurchase()
    {
        UIManager.Instance.OpenConfirmUI(PurchaseItem, null, "Do You Want To Purchase Gems?", "Gems Confirm", null);
    }
    private void PurchaseItem()
    {
        RuntimeDataManager.Instance.PlayerData.Gems += _gemsCount;
    }
}
