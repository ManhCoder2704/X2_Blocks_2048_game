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
    [SerializeField] private CanvasGroup[] _spellCanvas;
    [SerializeField] private Color _spellNormalColor;
    [SerializeField] private Color _spellActiveColor;
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private TMP_Text _diamondTxt;
    [SerializeField] private TMP_Text _highScoreTxt;
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
    }
    private void CheckSpell(int gems)
    {
        if (gems < 100)
        {
            _spellCanvas[0].alpha = 0.75f;
            _spellCanvas[0].interactable = false;
            _spellCanvas[1].alpha = 0.75f;
            _spellCanvas[1].interactable = false;
        }
        else
        {
            _spellCanvas[0].alpha = 1f;
            _spellCanvas[0].interactable = true;
            _spellCanvas[1].alpha = 1f;
            _spellCanvas[1].interactable = true;
        }
        if (gems < 400)
        {
            _spellCanvas[2].alpha = 0.75f;
            _spellCanvas[2].interactable = false;
        }
        else
        {
            _spellCanvas[2].alpha = 1f;
            _spellCanvas[2].interactable = true;
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
        _comboBG.DOFade(1, 0.25f)
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
        //TODO: Check user currency first
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
        _currentSkillType = SkillType.ClearOneRow;
        _currentSpellActiveBG = _spellClearBtn.image;
        _currentSpellActiveBG.color = _spellActiveColor;
        _spellBlinkTween = _spellClearImg.DOFade(0.25f, 0.3f).SetLoops(-1, LoopType.Yoyo);
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
        _currentSkillType = SkillType.RemoveOneBlock;
        _currentSpellActiveBG = _spellRemoveBtn.image;
        _currentSpellActiveBG.color = _spellActiveColor;
        _spellBlinkTween = _spellRemoveImg.DOFade(0.25f, 0.3f).SetLoops(-1, LoopType.Yoyo);
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
            _currentSpellActiveBG.color = _spellNormalColor;
        }
    }
}

public enum SkillType
{
    None,
    RemoveOneBlock,
    ClearOneRow,
    SwapNextBlock
}
