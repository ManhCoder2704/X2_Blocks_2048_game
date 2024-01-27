using System.Collections;
using UnityEngine;

public class RuntimeDataManager : Singleton<RuntimeDataManager>
{
    [SerializeField] private SettingData _settingData;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private MapData _mapData;
    [SerializeField] private LogData _logData;

    private void Awake()
    {
        StartCoroutine(LoadDataCO());
    }

    private IEnumerator LoadDataCO()
    {
        if (SaveManager.HasData<PlayerData>())
        {
            SaveManager.LoadData(ref _playerData);
        }
        else
        {
            // First time playing
            _playerData = new PlayerData();
            SaveManager.SaveData(_playerData);
        }
        yield return null;

        if (SaveManager.HasData<SettingData>())
        {
            SaveManager.LoadData(ref _settingData);
        }
        else
        {
            // First time playing
            _settingData = new SettingData();
            SaveManager.SaveData(_settingData);
        }
        yield return null;

        if (SaveManager.HasData<LogData>())
        {
            SaveManager.LoadData(ref _logData);
        }
        else
        {
            // First time playing
            _logData = new LogData();
            SaveManager.SaveData(_logData);
        }
        yield return null;

        if (SaveManager.HasData<MapData>())
        {
            SaveManager.LoadData(ref _mapData);
        }
        yield return null;
        UIManager.Instance.FirstLoadUI();
    }
}
