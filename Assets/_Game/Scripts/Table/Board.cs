using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Block _reviewBlock;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private Transform _blockContainer;
    [SerializeField] private DictionaryLib.Block_Coor_Dic _block_Coor_Dic;
    [SerializeField]
    private Block _nextBlock;
    public Block NextBlock => _nextBlock;
    [SerializeField]
    private Block _secondNextBlock;

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
        GetBlockInfo(_nextBlock);
        GetBlockInfo(_secondNextBlock);
    }

    public void OnBlockDrop()
    {
        _nextBlock.SpriteRenderer.color = _secondNextBlock.SpriteRenderer.color;
        _nextBlock.BlockNum.Number = _secondNextBlock.BlockNum.Number;
        GetBlockInfo(_secondNextBlock);
    }

    public Block GetNextBlock()
    {
        Block block = _blockPool.Pull();
        block.transform.SetParent(_blockContainer);
        block.SpriteRenderer.sortingOrder = 0;
        block.SpriteRenderer.color = _nextBlock.SpriteRenderer.color;
        block.BlockNum.Number = _nextBlock.BlockNum.Number;
        block.BlockNum.gameObject.SetActive(true);
        _reviewBlock.BlockNum.Number = _nextBlock.BlockNum.Number;
        _reviewBlock.SpriteRenderer.color = _nextBlock.SpriteRenderer.color;
        return block;
    }

    private void GetBlockInfo(Block block)
    {
        int randomNum = UnityEngine.Random.Range(1, 7);
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
}
