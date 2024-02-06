using System;
using TMPro;
using UnityEngine;
[Serializable]
public class RankBox : MonoBehaviour
{
    [SerializeField] private TMP_Text _rank;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    public void Init(string rank, string name, string score, string country)
    {
        _rank.text = rank;
        _name.text = $"#{country}: {name}";
        _score.text = score;
    }
}
