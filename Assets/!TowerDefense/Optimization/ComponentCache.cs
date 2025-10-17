using System.Collections.Generic;
using UnityEngine;

public class ComponentCache<T>  where T : Component
{
    private readonly Dictionary<GameObject, T> _cache = new();

    public  bool TryGetCached(GameObject obj, out T comp)
    {
        if (_cache.TryGetValue(obj, out comp)) return true;
        if (obj.TryGetComponent(out comp)) _cache[obj] = comp;
        return comp != null;
    }
}
