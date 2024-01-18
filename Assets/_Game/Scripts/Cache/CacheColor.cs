using System.Collections.Generic;
using UnityEngine;

public static class CacheColor
{
    private static Dictionary<int, Color> _cache = new Dictionary<int, Color>();

    public static Color GetColor(int seed)
    {
        if (_cache.TryGetValue(seed, out var color))
        {
            return color;
        }

        color = GetRandomColor(seed);
        _cache.Add(seed, color);
        return color;
    }

    private static Color GetRandomColor(int seed)
    {
        System.Random random = new System.Random(seed);
        float hue = (float)random.NextDouble();
        float saturation = Mathf.Max((float)random.NextDouble(), 0.5f);
        float value = Mathf.Max((float)random.NextDouble(), 0.5f);

        return Color.HSVToRGB(hue, saturation, value);
    }
}
