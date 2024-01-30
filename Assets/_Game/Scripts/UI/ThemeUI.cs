using System;
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
    private List<ThemeBox> _spawnedBoxes = new List<ThemeBox>();
    public static ThemeBox chosenThemeBox;
    private void Awake()
    {
        _themeChoosenIndex = RuntimeDataManager.Instance.SettingData.ThemeIndex;
        _purchasedTheme = RuntimeDataManager.Instance.SettingData.OwnThemeStatusList;
        SpawnButton();
    }
    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(_isPopup);
        RecheckThemeBox();
    }
    private void Start()
    {
        _escapeButton.onClick.AddListener(CloseTheme);
        OnGemChange(RuntimeDataManager.Instance.PlayerData.Gems);
        RuntimeDataManager.Instance.PlayerData.OnGemsChange += OnGemChange;
    }
    private void RecheckThemeBox()
    {
        List<bool> statusList = RuntimeDataManager.Instance.SettingData.OwnThemeStatusList;
        for (int i = 0; i < _spawnedBoxes.Count; i++)
        {
            _spawnedBoxes[i].CheckPurchased(statusList[i]);
        }
    }
    private void OnGemChange(int gems)
    {
        _gemsCountText.LerpNumber(gems);
    }
    private void SpawnButton()
    {
        BackGroundSO backgroundSO = RuntimeDataManager.Instance.BgSo;
        int count = backgroundSO.BackgroundListCount();
        bool chosen;
        for (int i = 0; i < count; i++)
        {
            chosen = i == _themeChoosenIndex;
            ThemeBox temp = Instantiate(_prefabThemeBox, _themeBoxContainer);
            temp.OnInit(backgroundSO.GetBackgroundByIndex(i), chosen, _purchasedTheme[i], i);
            _spawnedBoxes.Add(temp);
            if (!chosen) continue;
            chosenThemeBox = temp;
        }
    }

    private void CloseTheme()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
