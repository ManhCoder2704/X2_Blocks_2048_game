using System;
using UnityEngine;

public class DictionaryLib
{
    [Serializable]
    public class Block_Coor_Dic : SerializableDictionary<Vector2Int, Block> { }
}
