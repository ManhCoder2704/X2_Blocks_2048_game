using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Numerics;

public class RankUI : UIBase
{
    [SerializeField] private RectTransform _rankPanel;
    [SerializeField] private LeaderboardNavigator _leaderboardNavigator;
    [SerializeField] private RankBox _rankBoxPrefab_blue;
    [SerializeField] private RankBox _rankBoxPrefab_yellow;
    [SerializeField] private RankBox _myRank;
    [SerializeField] private Transform _rankBoxContainer;
    [SerializeField] private ScrollRect _rankScrollRect;
    private List<RankBox> rankBoxes;
    private List<RankData> _globalRankDatas;
    private List<RankData> _localRankDatas;
    private int _randomSeed;
    private bool _isLocalRank = true;
    private int _localRank;

    private float _duration = .5f;
    private void Awake()
    {
        CreateRankBox();
    }

    void OnEnable()
    {
        CheckRankSeed();
        if (IsPopup)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rankPanel.DOPivotY(0, 0f).SetEase(Ease.OutCubic));
            sequence.Append(_rankPanel.DOAnchorPosY(0, _duration).SetEase(Ease.OutCubic));
        }
    }

    void Start()
    {
        _leaderboardNavigator.Init(IsPopup ? CloseRank : null, ChangeBoard, ChangeBoard);
    }

    private void CheckRankSeed()
    {
        int seed = DateTime.UtcNow.DayOfYear.GetHashCode();
        bool hasConnection = CheckWiFiConnection();
        if (hasConnection)
        {
            if (_randomSeed != seed || _globalRankDatas == null || _localRankDatas == null)
            {
                _randomSeed = seed;
                StartCoroutine(GetDataFromApi());
            }
            else
                LoadPlayerData(hasConnection);
        }
        else
            LoadPlayerData(hasConnection);

    }

    private void CreateRankBox()
    {
        rankBoxes = new List<RankBox>();
        for (int i = 1; i < 11; i++)
        {
            RankBox rankBox;
            if (i % 2 == 0)
                rankBox = Instantiate(_rankBoxPrefab_yellow, _rankBoxContainer);
            else
                rankBox = Instantiate(_rankBoxPrefab_blue, _rankBoxContainer);
            rankBoxes.Add(rankBox);
        }
        Debug.Log(rankBoxes.Count);
    }

    private void ChangeBoard()
    {
        _isLocalRank = !_isLocalRank;
        LoadData();
    }

    private void LoadData()
    {
        if (_globalRankDatas == null || _localRankDatas == null) return;
        if (_isLocalRank)
        {
            for (int i = 0; i < 10; i++)
            {
                rankBoxes[i].Init((i + 1).ToString(), _localRankDatas[i].name, BigNumber.FormatBack(_localRankDatas[i].score), _localRankDatas[i].country);
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                rankBoxes[i].Init((i + 1).ToString(), _globalRankDatas[i].name, BigNumber.FormatBack(_globalRankDatas[i].score), _globalRankDatas[i].country);
            }
        }
        LoadPlayerData(CheckWiFiConnection());
    }

    private void LoadPlayerData(bool hasConnection)
    {
        BigInteger playerScore = RuntimeDataManager.Instance.PlayerData.HighScore.String2BigInterger();
        System.Random random = new System.Random(playerScore.GetHashCode());
        bool flag = (BigInteger.Compare(playerScore, 0) == 0 || !hasConnection);
        if (_isLocalRank)
        {
            _localRank = random.Next(100, short.MaxValue - playerScore.GetByteCount() * 8);
            _myRank.Init(flag ? "Unrank" : _localRank.ToString(), "You", RuntimeDataManager.Instance.PlayerData.HighScore.String2Point(), "VN");
        }
        else
        {
            _myRank.Init(flag ? "Unrank" : random.Next(Mathf.RoundToInt((float)(_localRank * random.NextDouble() - playerScore.GetByteCount() * 8)), 100000 - playerScore.GetByteCount() * 8).ToString(), "You", RuntimeDataManager.Instance.PlayerData.HighScore.String2Point(), "VN");
        }
    }

    private bool CheckWiFiConnection()
    {
        NetworkReachability reachability = Application.internetReachability;

        switch (reachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                return true; // Connected via WiFi
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                return true;
            default:
                return false; // Not connected via WiFi
        }
    }

    public void CloseRank()
    {
        UIManager.Instance.ClosePopup(this);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rankPanel.DOAnchorPosY(90, _duration).SetEase(Ease.OutCubic));
        sequence.Append(_rankPanel.DOPivotY(0.5f, 0f).SetEase(Ease.OutCubic));
    }

    private IEnumerator GetDataFromApi()
    {
        UIManager.Instance.StartLoading();

        // Create a Random instance
        System.Random random = new System.Random(_randomSeed);

        RankData[] globalRankData = new RankData[20];
        int i = 0;
        using (UnityWebRequest www = UnityWebRequest.Get($"https://randomuser.me/api/?results={10}&inc=name,nat&nat=us&seed={_randomSeed}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // Deserialize JSON response
                ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);

                // Access the data
                foreach (Result result in apiResponse.results)
                {
                    string name = $"{result.name.first} {result.name.last}";
                    Debug.Log(name);
                    globalRankData[i++] = new RankData(name, BigInteger.Parse(GenerateRandomScore(8, 15, random)), "VN");
                    yield return null;
                }
            }
        }

        using (UnityWebRequest www = UnityWebRequest.Get($"https://randomuser.me/api/?results={10}&inc=name,nat&seed={_randomSeed}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // Deserialize JSON response
                ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);

                // Access the data
                foreach (Result result in apiResponse.results)
                {
                    string name = $"{result.name.first} {result.name.last}";
                    Debug.Log(name);
                    globalRankData[i++] = new RankData(name, BigInteger.Parse(GenerateRandomScore(10, 20, random)), result.nat);
                    yield return null;
                }
            }
        }
        Array.Sort(globalRankData, (x, y) => BigInteger.Compare(y.score, x.score));
        _globalRankDatas = new List<RankData>(globalRankData);
        _localRankDatas = new List<RankData>();
        foreach (RankData data in _globalRankDatas)
        {
            if (data.country.Equals("VN"))
            {
                _localRankDatas.Add(data);
            }
        }


        _globalRankDatas.RemoveRange(10, 10);

        LoadData();

        UIManager.Instance.StopLoading();
    }

    private string GenerateRandomScore(int minLength, int maxLength, System.Random random)
    {
        // Generate a random length within the specified range
        int length = random.Next(minLength, maxLength + 1);

        // Generate a random string of digits
        string randomScore = "";
        for (int i = 0; i < length; i++)
        {
            randomScore += random.Next(10).ToString(); // Append a random digit (0-9)
        }

        return randomScore;
    }
}
[System.Serializable]
public class Name
{
    public string title;
    public string first;
    public string last;
}

[System.Serializable]
public class Result
{
    public Name name;
    public string nat;
}

[System.Serializable]
public class Info
{
    public string seed;
    public int results;
    public int page;
    public string version;
}

[System.Serializable]
public class ApiResponse
{
    public List<Result> results;
    public Info info;
}
