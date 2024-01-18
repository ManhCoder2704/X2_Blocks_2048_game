using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private Button _playBtn;

    void Start()
    {
        _playBtn.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        UIManager.Instance.OnPlayState();
    }
}
