using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : UIBase
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _questBtn;
    [SerializeField] private Transform _highScore;
    [SerializeField] private Button _diamonBtn;
    [SerializeField] private Button _highScoreBtn;

    void Awake()
    {
        _playBtn.onClick.AddListener(StartGame);
        _questBtn.onClick.AddListener(JoinQuest);
        _diamonBtn.onClick.AddListener(OnShop);
        _highScoreBtn.onClick.AddListener(OnRank);
    }

    private void OnRank()
    {
        UIManager.Instance.rankBtn.onClick.Invoke();
    }

    private void OnShop()
    {
        UIManager.Instance.shopBtn.onClick.Invoke();
    }

    private void JoinQuest()
    {
        throw new NotImplementedException();
    }

    private void StartGame()
    {
        Debug.Log("StartGame");
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }
}
