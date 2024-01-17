using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private byte _lineIndex;
    public byte LineIndex => _lineIndex;

    private byte _groundYCoordinate = 6;
    public byte GroundYCoordinate
    {
        get { return _groundYCoordinate; }
        set { _groundYCoordinate = (byte)Mathf.Min(value, 6); }
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
