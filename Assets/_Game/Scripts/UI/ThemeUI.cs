using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeUI : UIBase
{
    [SerializeField] private Button _escapeButton;
    [SerializeField] private ThemeBox _prefabThemeBox;
    [SerializeField] private Transform _themeBoxContainer;
    [SerializeField] private TMP_Text _gemsCountText;

    private int _themeChoosenIndex;
    private List<bool> _purchasedTheme;
    public static ThemeBox chosenThemeBox;
    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(_isPopup);
        _themeChoosenIndex = RuntimeDataManager.Instance.SettingData.ThemeIndex;
        _purchasedTheme = RuntimeDataManager.Instance.SettingData.OwnThemeStatusList;
    }
    private void Start()
    {
        _escapeButton.onClick.AddListener(CloseTheme);
        SpawnButton();
        OnGemChange(RuntimeDataManager.Instance.PlayerData.Gems);
        RuntimeDataManager.Instance.PlayerData.OnGemsChange += OnGemChange;
    }
    private void OnGemChange(int gems)
    {
        _gemsCountText.LerpNumber(gems);
    }
    private void SpawnButton()
    {
        StartCoroutine(SpawnButtonCO());
    }

    private IEnumerator SpawnButtonCO()
    {
        UIManager.Instance.StartLoading();
        BackGroundSO backgroundSO = RuntimeDataManager.Instance.BgSo;
        int count = backgroundSO.BackgroundListCount();
        bool chosen;
        for (int i = 0; i < count; i++)
        {
            chosen = i == _themeChoosenIndex;
            ThemeBox temp = Instantiate(_prefabThemeBox, _themeBoxContainer);
            temp.OnInit(backgroundSO.GetBackgroundByIndex(i), chosen, _purchasedTheme[i], i);
            if (!chosen) continue;
            chosenThemeBox = temp;
        }
        yield return new WaitForEndOfFrame();
        UIManager.Instance.StopLoading();
    }

    private void CloseTheme()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
