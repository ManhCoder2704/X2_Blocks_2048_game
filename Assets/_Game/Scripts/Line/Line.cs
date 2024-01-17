using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private byte _lineIndex;
    public byte LineIndex => _lineIndex;

    private byte _highestBlockIndex = 0;
    public byte HighestBlockIndex
    {
        get { return _highestBlockIndex; }
        set { _highestBlockIndex = value; }
    }

    public void OnMouseDown()
    {
        GameplayManager.Instance.OnMouseDown?.Invoke(this);
    }

    public void OnMouseEnter()
    {
        GameplayManager.Instance.OnMouseEnter?.Invoke(this);
    }
}
