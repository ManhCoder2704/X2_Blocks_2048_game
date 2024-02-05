using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    [SerializeField] private TMP_Text _gemsCountText;
    [SerializeField] private Button _escapeButton;
    [SerializeField] private Button _escapePanel;
    [SerializeField] private Transform _shopItemContainer;
    [SerializeField] private ShopItemBox _prefabShopItemBox;
    void Awake()
    {
        _escapeButton.onClick.AddListener(CloseShop);
        _escapePanel.onClick.AddListener(CloseShop);
        OnGemChange(RuntimeDataManager.Instance.PlayerData.Gems);
        RuntimeDataManager.Instance.PlayerData.OnGemsChange += OnGemChange;
        SpawnButton();
    }
    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(_isPopup);
        _escapePanel.gameObject.SetActive(_isPopup);
    }

    private void SpawnButton()
    {
        StartCoroutine(SpawnButtonCO());
    }

    private IEnumerator SpawnButtonCO()
    {
        UIManager.Instance.StartLoading();
        ShopItemSO shopItemSO = RuntimeDataManager.Instance.ShopItemSO;
        int count = shopItemSO.ShopItemListCount();
        for (int i = 0; i < count; i++)
        {
            ShopItemBox temp = Instantiate(_prefabShopItemBox, _shopItemContainer);
            temp.OnInit(shopItemSO.GetShopItemByIndex(i));
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        UIManager.Instance.StopLoading();
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
