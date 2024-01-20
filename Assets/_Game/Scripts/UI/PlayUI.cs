using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _adsBtn;
    [SerializeField] private Button _diamondBtn;
    [SerializeField] private Button _highscoreBtn;
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private TMP_Text _diamondTxt;
    [SerializeField] private TMP_Text _highScoreTxt;

    public TMP_Text ScoreTxt { get => _scoreTxt; set => _scoreTxt = value; }
    public TMP_Text DiamondTxt { get => _diamondTxt; set => _diamondTxt = value; }
    public TMP_Text HighScoreTxt { get => _highScoreTxt; set => _highScoreTxt = value; }

    void Start()
    {
        _pauseBtn.onClick.AddListener(() => UIManager.Instance.OnPausedState());
        _adsBtn.onClick.AddListener(() => UIManager.Instance.OnShopState());
        _diamondBtn.onClick.AddListener(() => UIManager.Instance.OnShopState());
        _highscoreBtn.onClick.AddListener(() => UIManager.Instance.OnProfileState());
    }
}
