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

    private int _minRandomBlockNumber = 1;
    private int _maxRandomBlockNumber = 3;

    public Block NextBlock => _nextBlock;

    public IDictionary<Vector2Int, Block> Block_Coor_Dic
    {
        get { return _block_Coor_Dic; }
        set { _block_Coor_Dic.CopyFrom(value); }
    }

    private ObjectPool<Block> _blockPool;

    private void Awake()
    {
        _blockPool = new ObjectPool<Block>(_blockPrefab, _blockContainer, 10);
    }

    private void Start()
    {
        GameplayManager.Instance.OnCombineBlock += OnCombineBlock;
        GetBlockInfo(_nextBlock);
        GetBlockInfo(_secondNextBlock);
    }

    public void OnBlockDrop()
    {
        _nextBlock.CopyValueFrom(_secondNextBlock);
        GetBlockInfo(_secondNextBlock);
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
        int randomNum = UnityEngine.Random.Range(_minRandomBlockNumber, _maxRandomBlockNumber);
        block.SpriteRenderer.color = CacheColor.GetColor(randomNum);
        block.BlockNum.Number = randomNum;
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

    public void SwapNextBlock()
    {
        _nextBlock.CopyValueFrom(_secondNextBlock);
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
}
