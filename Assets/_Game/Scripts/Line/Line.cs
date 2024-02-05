using DG.Tweening;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private byte _lineIndex;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Tween _changeColorTween;
    public byte LineIndex => _lineIndex;

    private int _groundYCoordinate = 6;
    public int GroundYCoordinate
    {
        get { return _groundYCoordinate; }
        set { _groundYCoordinate = Mathf.Min(value, 6); }
    }

    public void OnMouseDown()
    {
        GameplayManager.Instance.OnMouseDown?.Invoke(this);
    }

    public void OnMouseEnter()
    {
        GameplayManager.Instance.OnMouseEnter?.Invoke(this);
    }

    public void ChangeColorTo(int colorNumber = 0)
    {
        Color32 color;
        if (colorNumber == 0)
        {
            color = new Color32(20, 20, 20, 200);
            color.a = 200;
        }
        else
        {
            color = CacheColor.GetColor(colorNumber);
            color.a = 75;
        }
        _changeColorTween?.Kill();
        _changeColorTween = DOTween.To(() => _spriteRenderer.color, color => _spriteRenderer.color = color, color, 0.1f)
            .SetEase(Ease.Linear);
    }
}
