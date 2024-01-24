using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : Singleton<PlayUI>
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _adsBtn;
    [SerializeField] private Button _diamondBtn;
    [SerializeField] private Button _highscoreBtn;
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private TMP_Text _diamondTxt;
    [SerializeField] private TMP_Text _highScoreTxt;
    [SerializeField] private TMP_Text _comboText;

    public TMP_Text ComboText => _comboText;

    private BigInteger point = 0;

    void Awake()
    {
        GameplayManager.Instance.OnGetPoint += this.OnGetPoint;
        GameplayManager.Instance.OnReset += this.OnInit;
        _pauseBtn.onClick.AddListener(() => UIManager.Instance.OnPausedState());
        _adsBtn.onClick.AddListener(() => UIManager.Instance.OnShopState());
        _diamondBtn.onClick.AddListener(() => UIManager.Instance.OnShopState());
        _highscoreBtn.onClick.AddListener(() => UIManager.Instance.OnRankState());
        OnInit();
    }

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            Invoke(nameof(TurnOnTutorial), 0.01f);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
    }

    public void OnInit()
    {
        this.point = 0;
        _scoreTxt.text = "0";
    }
    private void OnGetPoint(int point)
    {
        this.point += BigInteger.Pow(2, point);
        _scoreTxt.FormatBack(this.point);
    }
    private void TurnOnTutorial()
    {
        UIManager.Instance.OnTutorialState();
    }
}
