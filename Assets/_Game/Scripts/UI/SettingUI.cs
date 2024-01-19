using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Button _vibraBtn;
    [SerializeField] private Button _musicBtn;
    [SerializeField] private Button _infoBtn;
    [SerializeField] private Button _rateBtn;
    [SerializeField] private Button _contactBtn;
    [SerializeField] private Button _likeBtn;
    [SerializeField] private Button _policyBtn;

    void Start()
    {
        _vibraBtn.onClick.AddListener(OnVibration);
        _musicBtn.onClick.AddListener(OnMusic);
        _infoBtn.onClick.AddListener(OnInfo);
        _rateBtn.onClick.AddListener(OnRate);
        _contactBtn.onClick.AddListener(OnContact);
        _likeBtn.onClick.AddListener(OnLike);
        _policyBtn.onClick.AddListener(OnPolicy);
    }

    private void OnPolicy()
    {
        throw new NotImplementedException();
    }

    private void OnLike()
    {
        throw new NotImplementedException();
    }

    private void OnContact()
    {
        throw new NotImplementedException();
    }

    private void OnRate()
    {
        throw new NotImplementedException();
    }

    private void OnInfo()
    {
        throw new NotImplementedException();
    }

    private void OnMusic()
    {
        throw new NotImplementedException();
    }

    private void OnVibration()
    {
        throw new NotImplementedException();
    }
}
