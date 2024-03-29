using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ThemeBox : MonoBehaviour
{
    [SerializeField] private Button _themeBtn;
    [SerializeField] private GameObject _status;
    [SerializeField] private GameObject _priceBox;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _priceTxt;

    private int _price;
    private Sprite _mainBG;
    private int _bgID;
    private bool _chosen;
    private bool _purchased;
    private void Start()
    {
        _themeBtn.onClick.AddListener(ChooseTheme);
    }
    public void OnInit(BackGroundData BGdata, bool chosen, bool owned, int id)
    {
        this._bgID = id;
        this._mainBG = BGdata.bgImage;
        this._chosen = chosen;
        this._purchased = owned;
        this._price = BGdata.price;

        _background.sprite = _mainBG;
        _status.SetActive(chosen);
        if (_price > 0 && !owned)
        {
            _priceTxt.text = _price.ToString();
        }
        else
        {
            _priceBox.SetActive(false);
        }
    }
    private void ChooseTheme()
    {
        if (_chosen) return;
        if (_purchased)
        {
            SelectBG();
        }
        else
        {
            CheckMoney();
        }
    }
    private void CheckMoney()
    {
        if (RuntimeDataManager.Instance.PlayerData.Gems < this._price)
        {
            UIManager.Instance.OpenNoticUI("You Don't Have Enough Money", "Notification");
            return;
        }
        ConfirmPurchase();
    }
    private void ConfirmPurchase()
    {
        UIManager.Instance.OpenConfirmUI(PurchaseBG, null, "Do You Want To Purchase This Theme?", "Theme Confirms", null);
    }
    private void PurchaseBG()
    {
        RuntimeDataManager.Instance.PlayerData.Gems -= this._price;
        this._purchased = true;
        _priceBox.SetActive(false);
        RuntimeDataManager.Instance.SettingData.OwnThemeStatusList[_bgID] = true;
        SelectBG();
    }
    private void SelectBG()
    {
        UIManager.Instance.ChangeBackground(_mainBG, _bgID);
        OnOffFrame(true);
        ThemeUI.chosenThemeBox.OnOffFrame(false);
        ThemeUI.chosenThemeBox = this;
    }
    private void OnOffFrame(bool chosen)
    {
        this._status.SetActive(chosen);
        this._chosen = chosen;
    }
    public void CheckPurchased(bool status)
    {
        this._purchased = status;
        this._priceBox.SetActive(!status);
    }
}
