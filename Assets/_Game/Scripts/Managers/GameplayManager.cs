using DG.Tweening;
using System;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] private Board _board;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private Block _reviewBlock;
    [SerializeField] private Transform _blockContainer;

    private Line _currentSelectLine;
    private Block _currentPendingBlock;
    private bool _isBlockMoving = false;

    public Action<Line> OnMouseDown;
    public Action<Line> OnMouseEnter;

    private void Awake()
    {
        OnMouseDown += OnLineMouseDown;
        OnMouseEnter += OnLineMouseEnter;
    }

    private void OnLineMouseDown(Line line)
    {
        if (_isBlockMoving) return;

        _currentSelectLine = line;

        _currentPendingBlock = Instantiate(_blockPrefab, _blockContainer);
        _currentPendingBlock.transform.position = new Vector3Int(line.LineIndex, 0);

        _reviewBlock.transform.position = new Vector3Int(line.LineIndex, 6 - line.HighestBlockIndex);
        _reviewBlock.gameObject.SetActive(true);
    }

    private void OnLineMouseEnter(Line line)
    {
        if (_currentSelectLine == null || _isBlockMoving) return;

        _currentSelectLine = line;
        _currentPendingBlock.transform.position = new Vector3Int(line.LineIndex, 0);

        _reviewBlock.transform.position = new Vector3Int(line.LineIndex, 6 - line.HighestBlockIndex);
    }

    private void OnLineMouseUp()
    {
        if (_currentSelectLine == null || _isBlockMoving || _currentSelectLine.HighestBlockIndex == 7) return;

        float posY = 6 - _currentSelectLine.HighestBlockIndex;
        _isBlockMoving = true;
        _reviewBlock.gameObject.SetActive(false);

        _board.Block_Coor_Dic.Add(new Vector2Int(_currentSelectLine.LineIndex, Mathf.FloorToInt(posY)), _currentPendingBlock);

        _currentPendingBlock.transform.DOMoveY(posY, posY * 0.1f).onComplete += () =>
        {
            _currentPendingBlock = null;
            _currentSelectLine.HighestBlockIndex++;
            _currentSelectLine = null;
            _isBlockMoving = false;
        };
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnLineMouseUp();
        }
    }
}
