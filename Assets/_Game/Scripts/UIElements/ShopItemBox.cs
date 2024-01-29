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

    private int _gemsCount;
    // Start is called before the first frame update
    void Start()
    {
        _shopItemBtn.onClick.AddListener(PurchaseItem);
    }

    public void OnInit(ShopItemData itemData)
    {
        _background.sprite = itemData.Image;
        float bonus = itemData.Bonus;
        if (bonus <= 0)
        {
            _bonusTxt.enabled = false;
        }
        _bonusTxt.text = $"+{bonus}% BONUS";
        CultureInfo culture = new CultureInfo("vi-VN");
        _priceTxt.text = itemData.Price.ToString("C", culture);
        _gemsCount = Mathf.FloorToInt(itemData.GemsCount * (1 + (bonus / 100f)));
        _gemCount.text = _gemsCount.ToString();
    }

    private void PurchaseItem()
    {
        RuntimeDataManager.Instance.PlayerData.Gems += _gemsCount;
    }
}
