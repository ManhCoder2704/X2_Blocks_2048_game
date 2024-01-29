using System;
using UnityEngine;

public class DictionaryLib
{
    [Serializable]
    public class Block_Coor_Dic : SerializableDictionary<Vector2Int, Block> { }
    [Serializable]
    public class Map_Data : SerializableDictionary<SerializeVector2Int, int> { }
}
