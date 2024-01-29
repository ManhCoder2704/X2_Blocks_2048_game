using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class RankUI : UIBase
{
    [SerializeField] private Button _switchBtn;
    [SerializeField] private Transform _toggleBtn;
    [SerializeField] private Button _escapeButton;

    private float _duration = 0.5f;

    void OnEnable()
    {
        _escapeButton.gameObject.SetActive(_isPopup);
    }
    void Start()
    {
        _switchBtn.onClick.AddListener(Switch);
        _escapeButton.onClick.AddListener(CloseRank);
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

    IEnumerator GetDataFromApi()
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"https://randomuser.me/api/?results={20}&inc=name,nat"))
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
                    string name = $"#{result.nat} {result.name.first} {result.name.last}";
                }
            }
        }
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
