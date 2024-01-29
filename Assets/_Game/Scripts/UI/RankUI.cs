using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class RankUI : UIBase
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private Transform _toggleBtn;
    [SerializeField] private Button _escapeButton;
    [SerializeField] private RankBox _rankBoxPrefab;
    [SerializeField] private RankBox _myRank;
    [SerializeField] private Transform _rankBoxContainer;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _ranking;
    [SerializeField] private Image _nation;

    private float _duration = 0.5f;

    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(_isPopup);
    }
    private void Awake()
    {
        OnHighScoreChange(RuntimeDataManager.Instance.PlayerData.HighScore);
        RuntimeDataManager.Instance.PlayerData.OnHighScoreChange += OnHighScoreChange;
    }

    void Start()
    {
        _switchBtn.onClick.AddListener(Switch);
        _escapeButton.onClick.AddListener(CloseRank);
        StartCoroutine(GetDataFromApi());
    }

    }
    private void OnHighScoreChange(string highScore)
    {
        _highScoreText.text = highScore;
    }
    private void Switch()
    {
        _switchBtn.interactable = false;
        _toggleBtn.DOLocalMoveX(-_toggleBtn.localPosition.x, _duration)
            .OnComplete(() =>
            {
                _switchBtn.interactable = true;
            });
    }

    private void CloseRank()
    {
        UIManager.Instance.ClosePopup(this);
    }

    private IEnumerator GetDataFromApi()
    {
        UIManager.Instance.StartLoading();
        // Number of people in the leaderboard (10 in this case)
        int leaderboardSize = 10;

        // Create a Random instance
        System.Random random = new System.Random(DateTime.UtcNow.DayOfYear.GetHashCode());

        // Assuming you have a minimum and maximum score length
        int minScoreLength = 10; // replace with your desired minimum score length
        int maxScoreLength = 30; // replace with your desired maximum score length

        using (UnityWebRequest www = UnityWebRequest.Get($"https://randomuser.me/api/?results={leaderboardSize}&inc=name,nat&seed=khanh"))
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
                int i = 1;
                foreach (Result result in apiResponse.results)
                {
                    string name = $"#{result.nat} {result.name.first} {result.name.last}";
                    Debug.Log(name);
                    RankBox rankBox = Instantiate(_rankBoxPrefab, _rankBoxContainer);
                    rankBox.Init(i++, name, GenerateRandomScore(minScoreLength, maxScoreLength, random).String2Point(), result.nat);
                    yield return null;
                }
            }
        }
        _myRank.Init(random.Next(100, short.MaxValue), RuntimeDataManager.Instance.PlayerData.PlayerName, RuntimeDataManager.Instance.PlayerData.HighScore.String2Point(), "VN");
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
