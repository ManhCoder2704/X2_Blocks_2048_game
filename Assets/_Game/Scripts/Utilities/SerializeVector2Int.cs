using System;
using UnityEngine;

[Serializable]
public class SerializeVector2Int
{
    public int x;
    public int y;

    public SerializeVector2Int(Vector2Int vector2Int)
    {
        this.x = vector2Int.x;
        this.y = vector2Int.y;
    }
}
