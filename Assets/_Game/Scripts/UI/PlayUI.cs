using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _adsBtn;

    void Start()
    {
        _pauseBtn.onClick.AddListener(PauseGame);
        _adsBtn.onClick.AddListener(OnShopState);
    }

    private void OnShopState()
    {
        UIManager.Instance.OnShopState();
    }

    private void PauseGame()
    {
        UIManager.Instance.OnPausedState();
    }
}
