using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : Singleton<HomeUI>
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _questBtn;
    [SerializeField] private Transform _highScore;
    [SerializeField] private Button _diamonBtn;
    [SerializeField] private Button _highScoreBtn;

    void Start()
    {
        _playBtn.onClick.AddListener(StartGame);
        _questBtn.onClick.AddListener(JoinQuest);
        _diamonBtn.onClick.AddListener(OnShop);
        _highScoreBtn.onClick.AddListener(OnProfile);
    }

    private void OnProfile()
    {
        UIManager.Instance.OnProfileState();
    }

    private void OnShop()
    {
        UIManager.Instance.OnShopState();
    }

    private void JoinQuest()
    {
        throw new NotImplementedException();
    }

    private void StartGame()
    {
        UIManager.Instance.OnPlayState();
    }
}
