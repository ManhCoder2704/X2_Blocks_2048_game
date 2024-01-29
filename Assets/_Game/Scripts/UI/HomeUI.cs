using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : UIBase
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _questBtn;
    [SerializeField] private Transform _highScore;
    [SerializeField] private Button _diamonBtn;
    [SerializeField] private Button _highScoreBtn;
    [SerializeField] private TMP_Text _gemsCountText;
    [SerializeField] private TMP_Text _highScoreText;

    void Awake()
    {
        _playBtn.onClick.AddListener(StartGame);
        _questBtn.onClick.AddListener(JoinQuest);
        _diamonBtn.onClick.AddListener(OnShop);
        _highScoreBtn.onClick.AddListener(OnRank);
        OnGemChange(RuntimeDataManager.Instance.PlayerData.Gems);
        RuntimeDataManager.Instance.PlayerData.OnGemsChange += OnGemChange;
        OnHighScoreChange(RuntimeDataManager.Instance.PlayerData.HighScore);
        RuntimeDataManager.Instance.PlayerData.OnHighScoreChange += OnHighScoreChange;
    }
    private void OnEnable()
    {
        _highScoreText.String2Point(RuntimeDataManager.Instance.PlayerData.HighScore);

    }
    private void OnGemChange(int gems)
    {
        _gemsCountText.LerpNumber(gems);
    }
    private void OnHighScoreChange(string highScore)
    {
        _highScoreText.text = highScore;
    }
    private void OnRank()
    {
        SoundManager.Instance.PlaySFX(SFXType.Click);
        UIManager.Instance.rankBtn.onClick.Invoke();
    }

    private void OnShop()
    {
        SoundManager.Instance.PlaySFX(SFXType.Click);
        UIManager.Instance.shopBtn.onClick.Invoke();
    }

    private void JoinQuest()
    {
        SoundManager.Instance.PlaySFX(SFXType.Click);
    }

    private void StartGame()
    {
        SoundManager.Instance.PlaySFX(SFXType.Start);
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }
}
