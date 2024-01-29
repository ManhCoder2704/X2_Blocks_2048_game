using TMPro;
using UnityEngine;

public class RankBox : MonoBehaviour
{
    [SerializeField] private TMP_Text _rank;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _country;

    public void Init(int rank, string name, string score, string country)
    {
        _rank.text = rank.ToString();
        _name.text = name;
        _score.text = score;
        _country.text = country;
    }
}
