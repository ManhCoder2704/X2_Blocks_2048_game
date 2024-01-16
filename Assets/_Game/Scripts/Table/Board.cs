using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private DictionaryLib.Block_Coor_Dic _block_Coor_Dic;
    public IDictionary<Vector2Int, Block> Block_Coor_Dic
    {
        get { return _block_Coor_Dic; }
        set { _block_Coor_Dic.CopyFrom(value); }
    }
}
