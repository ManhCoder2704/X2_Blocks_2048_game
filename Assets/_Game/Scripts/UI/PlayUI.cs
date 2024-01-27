using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayUI : UIBase
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _adsBtn;
    [SerializeField] private Button _diamondBtn;
    [SerializeField] private Button _highscoreBtn;
    [SerializeField] private Button _spellRemoveBtn;
    [SerializeField] private Button _spellClearBtn;
    [SerializeField] private Button _spellSwapBtn;
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private TMP_Text _diamondTxt;
    [SerializeField] private TMP_Text _highScoreTxt;
    [SerializeField] private TMP_Text _comboText;

    private BigInteger point = 0;

    void Awake()
    {
        GameplayManager.Instance.OnGetPoint += this.OnGetPoint;
        GameplayManager.Instance.OnReset += this.OnInit;
        _pauseBtn.onClick.AddListener(() => UIManager.Instance.OpenUI(UIType.PauseUI));
        _adsBtn.onClick.AddListener(() => UIManager.Instance.OpenUI(UIType.ShopUI));
        _diamondBtn.onClick.AddListener(() => UIManager.Instance.OpenUI(UIType.ShopPopupUI));
        _highscoreBtn.onClick.AddListener(() => UIManager.Instance.OpenUI(UIType.RankPopupUI));
        _spellRemoveBtn.onClick.AddListener(RemoveOneBlockOnClick);
        _spellClearBtn.onClick.AddListener(ClearOneRowOnclick);
        _spellSwapBtn.onClick.AddListener(SwapNextBlockOnClick);
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

    private void OnGetCombo(int comboCount)
    {
        _comboText.DOFade(1, 0.25f).OnComplete(() =>
        {
            _comboText.text = $"Combo +{comboCount}";
            _comboText.DOFade(0, 0.5f);
        });
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
        UIManager.Instance.OpenUI(UIType.TutorialUI);
    }

    [ContextMenu("SwapNextBlock")]
    private void SwapNextBlockOnClick()
    {
        //TODO: Check user currency first
        GameplayManager.Instance.ChangeSkillState(new SwapNextBlock());
    }

    [ContextMenu("ClearOneRow")]
    private void ClearOneRowOnclick()
    {
        //TODO: Check user currency first
        GameplayManager.Instance.ChangeSkillState(new ClearOneRow());
    }

    [ContextMenu("RemoveOneBlock")]
    private void RemoveOneBlockOnClick()
    {
        //TODO: Check user currency first
        GameplayManager.Instance.ChangeSkillState(new RemoveOneBlock());
    }
}
