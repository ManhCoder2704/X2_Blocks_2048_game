using DG.Tweening;
using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Block_Num _blockNum;
    public Block_Num BlockNum => _blockNum;

    private int _xCoor, _yCoor;
    public Vector2Int Coordinate { get => new Vector2Int(_xCoor, _yCoor); set { _xCoor = value.x; _yCoor = value.y; } }

    public void MoveTo(float yCoordinate, Action onComplete, Action onCompleted)
    {
        transform.DOMoveY(yCoordinate, yCoordinate * 0.1f).onComplete += () =>
        {
            onComplete?.Invoke();
            onCompleted?.Invoke();
        };
    }
}
