using System;
using System.Collections.Generic;
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

    private Queue<Block> additionAction = new Queue<Block>();
    private Queue<Vector2Int> emptyBlockCoor = new Queue<Vector2Int>();

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

        Action onComplete = null;

        if (_board.Block_Coor_Dic.ContainsKey(new Vector2Int(_currentSelectLine.LineIndex, Mathf.FloorToInt(posY + 1))))
        {
            Block aboveBlock = _board.Block_Coor_Dic[new Vector2Int(_currentSelectLine.LineIndex, Mathf.FloorToInt(posY + 1))];
            if (aboveBlock.BlockNum.Number == _currentPendingBlock.BlockNum.Number)
            {
                Debug.Log("Join");
                onComplete = () =>
                {
                    Destroy(aboveBlock.gameObject);
                    _board.Block_Coor_Dic[new Vector2Int(_currentSelectLine.LineIndex, Mathf.FloorToInt(posY + 1))] = _currentPendingBlock;
                    ++_currentPendingBlock.BlockNum.Number;
                    emptyBlockCoor.Enqueue(new Vector2Int(_currentSelectLine.LineIndex, Mathf.FloorToInt(posY + 1)));
                };
            }
            else
            {
                _board.Block_Coor_Dic.Add(new Vector2Int(_currentSelectLine.LineIndex, Mathf.FloorToInt(posY)), _currentPendingBlock);
            }
        }
        else
        {
            _board.Block_Coor_Dic.Add(new Vector2Int(_currentSelectLine.LineIndex, Mathf.FloorToInt(posY)), _currentPendingBlock);
        }


        _currentPendingBlock.MoveTo(posY, onComplete, () =>
        {
            if (emptyBlockCoor.Count != 0)
                AlignBlock();
            else
            {
                _isBlockMoving = false;
                _currentSelectLine.HighestBlockIndex++;
                _currentPendingBlock = null;
                _currentSelectLine = null;
            }
        });
    }

    private void AlignBlock()
    {
        while (emptyBlockCoor.Count != 0)
        {
            Vector2Int coor = emptyBlockCoor.Dequeue();
            for (int i = coor.y; i >= 0; i--)
            {
                if (_board.Block_Coor_Dic.ContainsKey(new Vector2Int(coor.x, i)))
                {
                    Block block = _board.Block_Coor_Dic[new Vector2Int(coor.x, i)];
                    _board.Block_Coor_Dic.Remove(new Vector2Int(coor.x, i));
                    _board.Block_Coor_Dic.Add(new Vector2Int(coor.x, i + 1), block);
                    block.MoveTo(i, null, () => { });
                    additionAction.Enqueue(block);
                    _currentSelectLine.HighestBlockIndex--;
                }
            }
        }
        _isBlockMoving = false;
        _currentSelectLine.HighestBlockIndex++;
        _currentPendingBlock = null;
        _currentSelectLine = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnLineMouseUp();
        }
    }
}
