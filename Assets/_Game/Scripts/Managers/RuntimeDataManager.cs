using System.Collections;
using UnityEngine;

public class RuntimeDataManager : Singleton<RuntimeDataManager>
{
    [SerializeField] private BackGroundSO _bgSo;
    [SerializeField] private SettingData _settingData;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private MapData _mapData;
    [SerializeField] private LogData _logData;

    public SettingData SettingData { get => _settingData; set => _settingData = value; }
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }
    public MapData MapData { get => _mapData; set => _mapData = value; }
    public LogData LogData { get => _logData; set => _logData = value; }
    public BackGroundSO BgSo { get => _bgSo; set => _bgSo = value; }

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
            _settingData = new SettingData(_bgSo.BackgroundListCount());
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
            Debug.Log("Has map data");
            yield return new WaitForSeconds(1f);
            SaveManager.LoadData(ref _mapData);
            yield return new WaitForEndOfFrame();
            GameplayManager.Instance.Board.ImportMapData(_mapData);
        }
        else
            GameplayManager.Instance.Board.ImportMapData(null);
        yield return null;
        UIManager.Instance.FirstLoadUI();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        SaveManager.SaveData(_playerData);
        SaveManager.SaveData(_settingData);
        SaveManager.SaveData(_logData);
        SaveManager.SaveData(GameplayManager.Instance.Board.ExportMapData());
    }
}
