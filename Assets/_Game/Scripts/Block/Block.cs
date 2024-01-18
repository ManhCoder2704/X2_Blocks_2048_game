using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class Block : MonoBehaviour
{
    [SerializeField] private Block_Num _blockNum;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Block_Num BlockNum => _blockNum;

    private Vector2Int _coordinate;
    public Vector2Int Coordinate { get => _coordinate; set => _coordinate = value; }

    private Line _currentLine;
    public Line CurrentLine { get => _currentLine; set => _currentLine = value; }

    public Tween MoveYTo(float yCoordinate)
    {
        return transform.DOMoveY(yCoordinate, yCoordinate * 0.1f)
            .OnComplete(() =>
            {
                Debug.Log($"{this.name} MoveYTo " + yCoordinate + " complete");
            });
    }

    public Tween MoveTo(Vector2Int coordinate)
    {
        return transform.DOMove(new Vector3Int(coordinate.x, coordinate.y), 0.1f)
            .OnStart(() =>
            {
                _blockNum.gameObject.SetActive(false);
            })
            .OnComplete(() =>
            {
                Debug.Log($"{this.name} MoveTo " + coordinate + " complete");
                this.gameObject.SetActive(false);
            });
    }

    public Tween ChangeColorTo(Color color)
    {
        return DOTween.To(() => _spriteRenderer.color, color => _spriteRenderer.color = color, color, 0.1f)
            .OnComplete(() => Debug.Log($"{this.name} color lerping complete"));
    }
}
