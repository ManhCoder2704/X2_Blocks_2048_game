using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Block _reviewBlock;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private Transform _blockContainer;
    [SerializeField] private DictionaryLib.Block_Coor_Dic _block_Coor_Dic;
    [SerializeField] private Block _nextBlock;
    [SerializeField] private Block _secondNextBlock;
    [SerializeField] private PointCounter _pointCounterPrefab;
    [SerializeField] private Transform _pointCounterContainer;
    [SerializeField] private Line[] _lines;

    private int _minRandomBlockNumber = 1;
    private int _maxRandomBlockNumber = 3;

    public Block NextBlock => _nextBlock;

    public IDictionary<Vector2Int, Block> Block_Coor_Dic
    {
        get { return _block_Coor_Dic; }
        set { _block_Coor_Dic.CopyFrom(value); }
    }

    private ObjectPool<Block> _blockPool;
    private ObjectPool<PointCounter> _pointCounterPool;

    private void Awake()
    {
        Debug.Log("Board Awake");
        //_blockPool = new ObjectPool<Block>(_blockPrefab, _blockContainer, 10);
        _pointCounterPool = new ObjectPool<PointCounter>(_pointCounterPrefab, _pointCounterContainer, 5);
    }

    private void Start()
    {
        GameplayManager.Instance.OnCombineBlock += OnCombineBlock;
    }
    private void OnInit()
    {
        GetBlockInfo(_nextBlock);
        GetBlockInfo(_secondNextBlock);
    }
    public void OnBlockDrop()
    {
        _nextBlock.CopyValueFrom(_secondNextBlock);
        GetBlockInfo(_secondNextBlock);
    }

    public PointCounter GetPointCounter()
    {
        PointCounter pointCounter = _pointCounterPool.Pull();
        pointCounter.transform.SetParent(_pointCounterContainer);
        return pointCounter;
    }

    public Block GetNextBlock()
    {
        Block block = _blockPool.Pull();
        block.transform.SetParent(_blockContainer);
        block.SpriteRenderer.sortingOrder = 0;
        block.CopyValueFrom(_nextBlock);
        block.BlockNum.gameObject.SetActive(true);
        _reviewBlock.CopyValueFrom(_nextBlock);
        return block;
    }

    private void GetBlockInfo(Block block)
    {
        //int randomNum = UnityEngine.Random.Range(_minRandomBlockNumber, _maxRandomBlockNumber);
        int randomNum = UnityEngine.Random.Range(1, 7);
        block.SpriteRenderer.color = CacheColor.GetColor(randomNum);
        block.BlockNum.Number = randomNum;
    }

    private void GetBlockInfo(Block block, int number)
    {
        block.SpriteRenderer.color = CacheColor.GetColor(number);
        block.BlockNum.Number = number;
    }

    public void SetReviewBlockCoor(Vector3Int coor, bool visible)
    {
        _reviewBlock.transform.position = coor;
        _reviewBlock.gameObject.SetActive(visible);
    }

    public void DisableReviewBlock()
    {
        _reviewBlock.gameObject.SetActive(false);
    }

    /// <summary>
    /// Skill: Swap next block with second next block
    /// </summary>
    public void SwapNextBlock()
    {
        _nextBlock.SwapValueWith(_secondNextBlock);
        GameplayManager.Instance.IsBlockMoving = false;
    }

    /// <summary>
    /// Skill: Clear one row
    /// </summary>
    public void ClearOneRow(Vector2Int? inputCoor, List<Block> actionBlocks, Action callback)
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < 5; i++)
        {
            if (_block_Coor_Dic.TryGetValue(new Vector2Int(i, inputCoor.Value.y), out Block block))
            {
                block.CurrentLine.GroundYCoordinate = block.Coordinate.y;
                _block_Coor_Dic.Remove(block.Coordinate);
                sequence.Join(block.RemoveFromBoard());

                GameplayManager.Instance.QuantityBlock--;

                for (int j = inputCoor.Value.y - 1; j >= 0; j--)
                {
                    if (_block_Coor_Dic.TryGetValue(new Vector2Int(i, j), out Block dropBlock))
                    {
                        Debug.Log($"Add drop block {1 << dropBlock.BlockNum.Number} with coor: {dropBlock.Coordinate}");
                        actionBlocks.Add(dropBlock);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        sequence.OnComplete(() =>
        {
            callback?.Invoke();
            GameplayManager.Instance.ChangeSkillState(null);
        });
    }

    /// <summary>
    /// Remove one block
    /// </summary>
    public void RemoveOneBlock(Vector2Int? inputCoor, List<Block> actionBlocks, Action callback)
    {
        if (_block_Coor_Dic.TryGetValue(new Vector2Int(inputCoor.Value.x, inputCoor.Value.y), out Block block))
        {
            block.CurrentLine.GroundYCoordinate = block.Coordinate.y;
            _block_Coor_Dic.Remove(block.Coordinate);
            block.RemoveFromBoard().OnComplete(() =>
            {
                callback?.Invoke();
                GameplayManager.Instance.ChangeSkillState(null);
            });

            GameplayManager.Instance.QuantityBlock--;

            for (int j = inputCoor.Value.y - 1; j >= 0; j--)
            {
                if (_block_Coor_Dic.TryGetValue(new Vector2Int(inputCoor.Value.x, j), out Block dropBlock))
                {
                    Debug.Log($"Add drop block {1 << dropBlock.BlockNum.Number} with coor: {dropBlock.Coordinate}");
                    actionBlocks.Add(dropBlock);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void OnCombineBlock(int number)
    {
        if (number > _maxRandomBlockNumber)
        {
            _maxRandomBlockNumber = number;
            if (_maxRandomBlockNumber - _minRandomBlockNumber > 6)
            {
                _minRandomBlockNumber = _maxRandomBlockNumber - 6;
                // TODO: Increase difficulty
            }
        }

    }

    public void ResetBoard()
    {
        foreach (Block item in _block_Coor_Dic.Values)
        {
            item.CurrentLine.GroundYCoordinate = 6;
            item.ReturnToPool();
        }
        if (SaveManager.HasData<MapData>())
        {
            SaveManager.DeleteData<MapData>();
        }
        _block_Coor_Dic.Clear();
        OnInit();
    }

    public MapData ExportMapData()
    {
        // Save data
        MapData mapData = new MapData();
        mapData.NextBlock = _nextBlock.BlockNum.Number;
        mapData.SecondNextBlock = _secondNextBlock.BlockNum.Number;
        mapData.Score = GameplayManager.Instance.Point.ToString();
        foreach (KeyValuePair<Vector2Int, Block> item in _block_Coor_Dic)
        {
            mapData.LevelData.Add(new SerializeVector2Int(item.Key), item.Value.BlockNum.Number);
        }
        return mapData;
    }

    public void ImportMapData(MapData mapData)
    {
        // Load data
        _blockPool = new ObjectPool<Block>(_blockPrefab, _blockContainer, 10);
        if (mapData == null)
        {
            OnInit();
            return;
        }
        int quantityBlock = 0;
        GetBlockInfo(_nextBlock, mapData.NextBlock);
        GetBlockInfo(_secondNextBlock, mapData.SecondNextBlock);
        GameplayManager.Instance.Point = System.Numerics.BigInteger.Parse(mapData.Score);
        foreach (KeyValuePair<SerializeVector2Int, int> item in mapData.LevelData)
        {
            quantityBlock++;
            Vector2Int coor = new Vector2Int(item.Key.x, item.Key.y);
            Block block = _blockPool.Pull();
            block.transform.SetParent(_blockContainer);
            block.SpriteRenderer.sortingOrder = 0;
            block.SpriteRenderer.color = CacheColor.GetColor(item.Value);
            block.BlockNum.Number = item.Value;
            block.CurrentLine = _lines[coor.x];
            block.CurrentLine.GroundYCoordinate--;
            block.transform.position = new Vector3(coor.x, coor.y);
            _block_Coor_Dic.Add(coor, block);
            block.gameObject.SetActive(true);
        }
        GameplayManager.Instance.QuantityBlock = quantityBlock;
    }
}
