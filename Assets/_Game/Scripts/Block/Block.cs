using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class Block : MonoBehaviour, IPoolable<Block>
{
    [SerializeField] private Block_Num _blockNum;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Block_Num BlockNum => _blockNum;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    private Vector2Int _coordinate;
    public Vector2Int Coordinate { get => _coordinate; set => _coordinate = value; }

    private Line _currentLine;
    public Line CurrentLine { get => _currentLine; set => _currentLine = value; }

    private Action<Block> _returnAction;

    public Tween MoveYTo(float yCoordinate)
    {
        return transform.DOMoveY(yCoordinate, yCoordinate * 0.05f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log($"{1 << this.BlockNum.Number} MoveYTo " + yCoordinate + " complete");
            });
    }

    public Tween MoveTo(Vector2Int coordinate)
    {
        return transform.DOMove(new Vector3Int(coordinate.x, coordinate.y), 0.25f)
            .SetEase(Ease.Linear)
            .OnStart(() =>
            {
                _spriteRenderer.sortingOrder = 1;
                _blockNum.gameObject.SetActive(false);
            })
            .OnComplete(() =>
            {
                Debug.Log($"{1 << this.BlockNum.Number} MoveTo " + coordinate + " complete");
                ReturnToPool();
            });
    }

    public Tween ChangeColorTo(int colorNumber, bool isAdmin)
    {
        Color color = CacheColor.GetColor(colorNumber);
        return DOTween.To(() => _spriteRenderer.color, color => _spriteRenderer.color = color, color, 0.25f)
            .SetEase(Ease.Linear)
            .OnStart(() =>
            {
                if (isAdmin)
                {
                    transform.DOScale(1, 0.125f).SetLoops(2, LoopType.Yoyo);
                }
            })
            .OnComplete(() =>
            {
                this.BlockNum.Number = colorNumber;
                GameplayManager.Instance.OnCombineBlock?.Invoke(this.BlockNum.Number);
            });
    }

    public void Initialize(Action<Block> returnAction)
    {
        _returnAction = returnAction;
    }

    public void ReturnToPool()
    {
        _returnAction?.Invoke(this);
    }

    public void CopyValueFrom(Block other)
    {
        _blockNum.Number = other.BlockNum.Number;
        _spriteRenderer.color = other.SpriteRenderer.color;
    }

    public void SwapValueWith(Block other)
    {
        int tempNumber = _blockNum.Number;
        Color tempColor = _spriteRenderer.color;

        CopyValueFrom(other);

        other.BlockNum.Number = tempNumber;
        other.SpriteRenderer.color = tempColor;
    }
}
