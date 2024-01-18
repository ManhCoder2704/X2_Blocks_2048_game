using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Progress;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] private Board _board;

    private Line _currentSelectLine;
    private Block _currentPendingBlock;
    private bool _isBlockMoving = false;

    public Action<Line> OnMouseDown;
    public Action<Line> OnMouseEnter;

    private int _touchCount = 0;

    [SerializeField]
    private List<Block> _actionBlocks = new List<Block>();

    private void Awake()
    {
        OnMouseDown += OnLineMouseDown;
        OnMouseEnter += OnLineMouseEnter;
    }

    private void OnLineMouseDown(Line line)
    {
        if (_isBlockMoving) return;
        _currentPendingBlock = _board.GetBlock();
        _currentPendingBlock.name = "Block " + _touchCount++;
        PendingShoot(line);
    }

    private void OnLineMouseEnter(Line line)
    {
        if (_currentSelectLine == null || _isBlockMoving) return;
        PendingShoot(line);
    }
    private void PendingShoot(Line line)
    {
        _currentSelectLine = line;
        _currentPendingBlock.transform.position = new Vector3Int(line.LineIndex, 0);
        _board.SetReviewBlockCoor(new Vector3Int(line.LineIndex, _currentSelectLine.GroundYCoordinate));
        _currentPendingBlock.Coordinate = new Vector2Int(line.LineIndex, _currentSelectLine.GroundYCoordinate);
        _currentPendingBlock.CurrentLine = line;
    }
    private void OnLineMouseUp()
    {
        if (_currentSelectLine == null || _isBlockMoving) return;

        _isBlockMoving = true;
        _board.DisableReviewBlock();

        _currentPendingBlock.Coordinate = new Vector2Int(_currentPendingBlock.Coordinate.x, 0);
        _actionBlocks.Add(_currentPendingBlock);

        _currentSelectLine = null;
        _currentPendingBlock = null;

        BlockDropState();
    }

    private void BlockDropState()
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < _actionBlocks.Count; i++)
        {
            Block block = _actionBlocks[i];
            Vector2Int newCoordinate = new Vector2Int(block.Coordinate.x, block.CurrentLine.GroundYCoordinate);
            Debug.Log(block.Coordinate + " " + newCoordinate);
            if (block.Coordinate.y > newCoordinate.y)
            {
                continue;
            }
            _board.Block_Coor_Dic.Remove(block.Coordinate);
            _board.Block_Coor_Dic.Add(newCoordinate, block);

            block.Coordinate = newCoordinate;
            block.CurrentLine.GroundYCoordinate--;

            sequence.Join(block.MoveYTo(newCoordinate.y));
        }
        sequence.OnComplete(() =>
        {
            BlockCombineState();
        });
    }

    private void BlockCombineState()
    {
        HashSet<Block> pendingBlocks = new HashSet<Block>();
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < _actionBlocks.Count; i++)
        {
            //bool hasTopBlock = false;
            List<Block> combineBlocks = FindSimilarBlockAround(_actionBlocks[i]);

            // No similar block around then remove this block and continue
            if (combineBlocks.Count == 0)
            {
                _actionBlocks.RemoveAt(i);
                i--;
                continue;
            }

            // Add this block to combine list
            if (combineBlocks[0].Coordinate.y == _actionBlocks[i].Coordinate.y)
            {
                combineBlocks.Insert(0, _actionBlocks[i]);
            }
            else
            {
                //hasTopBlock = true;
                combineBlocks.Add(_actionBlocks[i]);
            }

            // Find the block that has the most similar blocks around
            int maxValue = 0;
            Block maxBlock = null;
            List<Block> maxCombineBlockRelative = new List<Block>();

            for (int j = 0; j < combineBlocks.Count; j++)
            {
                List<Block> temp = FindSimilarBlockAround(combineBlocks[j]);
                if (temp.Count > maxValue)
                {
                    maxValue = temp.Count;
                    maxBlock = combineBlocks[j];
                    maxCombineBlockRelative = temp;
                }
            }
            Debug.Log($"Max combine block is {maxBlock.name} with coor: {maxBlock.Coordinate}");
            Debug.Log($"Current combine block is {_actionBlocks[i].name} with coor: {_actionBlocks[i].Coordinate}");
            maxBlock.BlockNum.Number += maxValue;
            sequence.Join(maxBlock.ChangeColorTo(CacheColor.GetColor(maxBlock.BlockNum.Number)));

            // Setup combine sequence
            foreach (var item in maxCombineBlockRelative)
            {
                // dont ask why this happen :)) i have no idea why it work perfectly
                for (int k = item.Coordinate.y - 1; k > 0; k--)
                {
                    Block block = null;
                    if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(item.Coordinate.x, k), out block))
                    {
                        if (block == maxBlock)
                        {
                            break;
                        }
                        pendingBlocks.Add(block);
                    }
                    else
                    {
                        break;
                    }
                }

                // Remove block from board info
                item.CurrentLine.GroundYCoordinate = (byte)item.Coordinate.y;
                item.CurrentLine = maxBlock.CurrentLine;

                _board.Block_Coor_Dic.Remove(item.Coordinate);

                // Remove block from action list
                if (_actionBlocks.Contains(item) && item != _actionBlocks[i])
                {
                    int index = _actionBlocks.IndexOf(item);
                    if (index < i)
                    {
                        i--;
                    }
                    _actionBlocks.Remove(item);
                }

                // Combine block
                sequence.Join(item.ChangeColorTo(CacheColor.GetColor(maxBlock.BlockNum.Number)));
                sequence.Join(item.MoveTo(maxBlock.Coordinate));
            }

            // if current action block is not the max block then replace it
            if (maxBlock != _actionBlocks[i]) ///////////// error here
            {
                _actionBlocks[i] = maxBlock;
            }

        }
        sequence.OnComplete(() =>
        {
            Debug.Log("Combine complete: " + _actionBlocks.Count);
            _actionBlocks.AddRange(pendingBlocks);
            if (_actionBlocks.Count > 0)
            {
                BlockDropState();
            }
            else
            {
                _isBlockMoving = false;
            }
        });
    }

    private List<Block> FindSimilarBlockAround(Block block)
    {
        List<Block> blocks = new List<Block>();
        Block tempBlock = null;
        if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(block.Coordinate.x, block.Coordinate.y + 1), out tempBlock)) // above
        {
            if (tempBlock.BlockNum.Number == block.BlockNum.Number)
            {
                blocks.Add(tempBlock);
            }
        }
        if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(block.Coordinate.x, block.Coordinate.y - 1), out tempBlock)) // above
        {
            if (tempBlock.BlockNum.Number == block.BlockNum.Number)
            {
                blocks.Add(tempBlock);
            }
        }
        if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(block.Coordinate.x - 1, block.Coordinate.y), out tempBlock)) // left
        {
            if (tempBlock.BlockNum.Number == block.BlockNum.Number)
            {
                blocks.Add(tempBlock);
            }
        }
        if (_board.Block_Coor_Dic.TryGetValue(new Vector2Int(block.Coordinate.x + 1, block.Coordinate.y), out tempBlock)) // right
        {
            if (tempBlock.BlockNum.Number == block.BlockNum.Number)
            {
                blocks.Add(tempBlock);
            }
        }
        return blocks;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnLineMouseUp();
        }
    }
}
