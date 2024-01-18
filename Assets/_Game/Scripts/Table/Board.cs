using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Block _reviewBlock;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private Transform _blockContainer;
    [SerializeField] private DictionaryLib.Block_Coor_Dic _block_Coor_Dic;
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

    public Block GetBlock()
    {
        int randomNum = UnityEngine.Random.Range(1, 7);
        Block temp = _blockPool.Pull();
        temp.SpriteRenderer.sortingOrder = 0;
        temp.SpriteRenderer.color = CacheColor.GetColor(randomNum);
        temp.BlockNum.Number = randomNum;
        temp.BlockNum.gameObject.SetActive(true);
        _reviewBlock.BlockNum.Number = randomNum;
        _reviewBlock.SpriteRenderer.color = CacheColor.GetColor(randomNum);
        _reviewBlock.gameObject.SetActive(true);
        return temp;
    }

    public void SetReviewBlockCoor(Vector3Int coor)
    {
        _reviewBlock.transform.position = coor;
    }

    public void DisableReviewBlock()
    {
        _reviewBlock.gameObject.SetActive(false);
    }
}
