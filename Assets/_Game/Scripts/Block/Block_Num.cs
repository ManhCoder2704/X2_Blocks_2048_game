using TMPro;
using UnityEngine;

public class Block_Num : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberDisplay;
    public void SetNumber(int number)
    {
        _numberDisplay.text = (1 << number).ToString();
    }
}
