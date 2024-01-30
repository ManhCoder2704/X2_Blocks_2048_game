using TMPro;
using UnityEngine;

public class RankBox : MonoBehaviour
{
    [SerializeField] private TMP_Text _rank;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _country;

    public void Init(string rank, string name, string score, string country)
    {
        _rank.text = rank;
        _name.text = name;
        _score.text = score;
        _country.text = country;
    }
}
