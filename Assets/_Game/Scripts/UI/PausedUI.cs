using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _vibraBtn;
    [SerializeField] private Button _musicBtn;
    void Start()
    {
        _homeBtn.onClick.AddListener(OnHome);
        _continueBtn.onClick.AddListener(Continue);
        _restartBtn.onClick.AddListener(Restart);
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
    }

    private void OnMusic()
    {
        throw new NotImplementedException();
    }

    private void OnVibration()
    {
        throw new NotImplementedException();
    }

    private void Restart()
    {
        throw new NotImplementedException();
    }

    private void Continue()
    {
        throw new NotImplementedException();
    }

    private void OnHome()
    {
        UIManager.Instance.OnHomeState();
    }
}
