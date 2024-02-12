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
    [SerializeField] private Image _spellRemoveImg;
    [SerializeField] private Image _spellClearImg;
    [SerializeField] private GameObject[] _spellMask;
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private TMP_Text _comboText;
    [SerializeField] private Image _comboBG;
    [SerializeField] private TMP_Text _gemsCountText;
    [SerializeField] private TMP_Text _highScoreText;

    private SkillType _currentSkillType = SkillType.None;
    private Tween _spellBlinkTween;
    private Image _currentSpellActiveBG;

    void Awake()
    {
        GameplayManager.Instance.OnGetPoint += this.OnGetPoint;
        GameplayManager.Instance.OnReset += this.OnInit;
        GameplayManager.Instance.OnGetCombo += this.OnGetCombo;
        _pauseBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(SFXType.Click);
            UIManager.Instance.OpenUI(UIType.PauseUI);
        });
        _adsBtn.onClick.AddListener(() =>
            {
                //Watch Ads
                RuntimeDataManager.Instance.PlayerData.Gems += 100;
            }
            );

        _diamondBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(SFXType.Click);
            UIManager.Instance.OpenUI(UIType.ShopPopupUI);
        });
        _highscoreBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(SFXType.Click);
            UIManager.Instance.OpenUI(UIType.RankPopupUI);
        });
        _spellRemoveBtn.onClick.AddListener(RemoveOneBlockOnClick);
        _spellClearBtn.onClick.AddListener(ClearOneRowOnclick);
        _spellSwapBtn.onClick.AddListener(SwapNextBlockOnClick);
        GameplayManager.Instance.OnUseSkill += KillBlickTween;
        OnGemChange(RuntimeDataManager.Instance.PlayerData.Gems);
        RuntimeDataManager.Instance.PlayerData.OnGemsChange += OnGemChange;
        RuntimeDataManager.Instance.PlayerData.OnGemsChange += CheckSpell;
        OnHighScoreChange(RuntimeDataManager.Instance.PlayerData.HighScore);
        RuntimeDataManager.Instance.PlayerData.OnHighScoreChange += OnHighScoreChange;
        CheckSpell(RuntimeDataManager.Instance.PlayerData.Gems);
        this.gameObject.SetActive(false);
    }
    private void CheckSpell(int gems)
    {
        if (gems < 100)
        {
            _spellMask[0].SetActive(true);
            _spellMask[2].SetActive(true);
        }
        else
        {
            _spellMask[0].SetActive(false);
            _spellMask[2].SetActive(false);
        }
        if (gems < 400)
        {
            _spellMask[1].SetActive(true);
        }
        else
        {
            _spellMask[1].SetActive(false);
        }
    }

    private void OnGemChange(int gems)
    {
        _gemsCountText.LerpNumber(gems);
    }
    private void OnHighScoreChange(string highScore)
    {
        _highScoreText.text = highScore;
    }
    private void OnEnable()
    {
        UIManager.Instance.OnOffBG(true);
        _highScoreText.String2Point(RuntimeDataManager.Instance.PlayerData.HighScore);
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            Invoke(nameof(TurnOnTutorial), 0.01f);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        OnGetPoint(GameplayManager.Instance.Point);
    }

    private void OnGetCombo(int comboCount)
    {
        _comboText.text = $"Combo +{comboCount}";
        SoundManager.Instance.PlaySFX(SFXType.Combo);
        _comboBG.DOFade(0.6f, 0.25f)
            .OnComplete(() =>
            {
                _comboBG.DOFade(0, 0.75f)
                .SetDelay(1f);
            });
        _comboText.DOFade(1, 0.25f)
            .OnComplete(() =>
            {
                _comboText
                .DOFade(0, .75f)
                .SetDelay(1f);
            });
    }

    public void OnInit()
    {
        _scoreTxt.text = "0";
    }
    private void OnGetPoint(BigInteger point)
    {
        _scoreTxt.FormatBack(point);
    }

    private void TurnOnTutorial()
    {
        UIManager.Instance.OpenUI(UIType.TutorialUI);
    }

    [ContextMenu("SwapNextBlock")]
    private void SwapNextBlockOnClick()
    {
        if (GameplayManager.Instance.QuantityBlock == 0 || GameplayManager.Instance.IsBlockMoving) return;
        if (RuntimeDataManager.Instance.PlayerData.Gems < 100) return;
        _currentSkillType = SkillType.SwapNextBlock;
        GameplayManager.Instance.ChangeSkillState(new SwapNextBlock());
    }

    [ContextMenu("ClearOneRow")]
    private void ClearOneRowOnclick()
    {
        if (GameplayManager.Instance.QuantityBlock == 0 || GameplayManager.Instance.IsBlockMoving) return;
        if (_currentSkillType == SkillType.ClearOneRow)
        {
            GameplayManager.Instance.ChangeSkillState(null);
            KillBlickTween();
            return;
        }
        KillBlickTween();
        if (RuntimeDataManager.Instance.PlayerData.Gems < 400) return;
        BlinkSkillBG(SkillType.ClearOneRow, _spellClearBtn.image);
        //TODO: Check user currency first
        GameplayManager.Instance.ChangeSkillState(new ClearOneRow());
    }

    [ContextMenu("RemoveOneBlock")]
    private void RemoveOneBlockOnClick()
    {
        if (GameplayManager.Instance.QuantityBlock == 0 || GameplayManager.Instance.IsBlockMoving) return;
        if (_currentSkillType == SkillType.RemoveOneBlock)
        {
            GameplayManager.Instance.ChangeSkillState(null);
            KillBlickTween();
            return;
        }
        KillBlickTween();
        if (RuntimeDataManager.Instance.PlayerData.Gems < 100) return;
        BlinkSkillBG(SkillType.RemoveOneBlock, _spellRemoveBtn.image);
        //TODO: Check user currency first
        GameplayManager.Instance.ChangeSkillState(new RemoveOneBlock());
    }

    private void KillBlickTween()
    {
        _currentSkillType = SkillType.None;
        _spellBlinkTween?.Restart();
        _spellBlinkTween?.Kill();
        if (_currentSpellActiveBG != null)
        {
            // _currentSpellActiveBG.color = _spellNormalColor;
        }
    }

    private void BlinkSkillBG(SkillType skill, Image imageBG)
    {
        _currentSkillType = skill;
        _currentSpellActiveBG = imageBG;
        _spellBlinkTween = _currentSpellActiveBG.DOFade(0.25f, 0.3f).SetLoops(-1, LoopType.Yoyo);
    }
}

public enum SkillType
{
    None,
    RemoveOneBlock,
    ClearOneRow,
    SwapNextBlock
}
