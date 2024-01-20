using TMPro;
using UnityEngine;

public class Block_Num : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberDisplay;
    private int _number = 1;
    public int Number
    {
        get { return _number; }
        set
        {
            _number = value;
            SetNumber(_number);
        }
    }
    private void SetNumber(int number)
    {
        _numberDisplay.FormatLargeNumberPowerOfTwo(number);
    }
}
