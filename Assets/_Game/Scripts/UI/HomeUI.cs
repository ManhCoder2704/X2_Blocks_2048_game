using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : UIBase
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _diamonBtn;
    [SerializeField] private Button _highScoreBtn;
    [SerializeField] private Image _highestBlockImage;
    [SerializeField] private TMP_Text _highBlockText;
    [SerializeField] private TMP_Text _gemsCountText;
    [SerializeField] private TMP_Text _highScoreText;

    void Awake()
    {
        _playBtn.onClick.AddListener(StartGame);
        _diamonBtn.onClick.AddListener(OnShop);
        _highScoreBtn.onClick.AddListener(OnRank);
        OnGemChange(RuntimeDataManager.Instance.PlayerData.Gems);
        RuntimeDataManager.Instance.PlayerData.OnGemsChange += OnGemChange;
        OnHighScoreChange(RuntimeDataManager.Instance.PlayerData.HighScore);
        RuntimeDataManager.Instance.PlayerData.OnHighScoreChange += OnHighScoreChange;
    }
    private void OnEnable()
    {
        _highScoreText.String2Point(RuntimeDataManager.Instance.PlayerData.HighScore);
        int highestBlock = RuntimeDataManager.Instance.PlayerData.HighestBlockIndex;
        if (highestBlock > 0)
        {
            _highestBlockImage.gameObject.SetActive(true);
            _highBlockText.FormatLargeNumberPowerOfTwo(highestBlock);
            //_highBlockText.color = CacheColor.GetColor(highestBlock);
            //_highestBlockImage.color = CacheColor.GetColor(highestBlock);
        }
        else
        {
            _highestBlockImage.gameObject.SetActive(false);
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
    private void OnRank()
    {
        UIManager.Instance.MenuNavigatorBar.OpenRank();
    }

    private void OnShop()
    {
        UIManager.Instance.MenuNavigatorBar.OpenShop();
    }

    private void StartGame()
    {
        SoundManager.Instance.PlaySFX(SFXType.Start);
        UIManager.Instance.OpenUI(UIType.PlayUI);
    }
}
