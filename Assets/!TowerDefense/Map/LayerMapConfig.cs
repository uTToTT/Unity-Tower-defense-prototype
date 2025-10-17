using System.Collections.Generic;
using UnityEngine;

public readonly struct LayerMapConfig
{
    public Vector2Int Start { get; }
    public Vector2Int Finish { get; }
    public string Path { get; }
    public List<Vector2Int> ComputedPath { get; }
    public LayerMapConfig(Vector2Int start, Vector2Int finish, string path)
    {
        Start = start; 
        Finish = finish;
        Path = path;

        ComputedPath = JSONMapParser.ParsePath(start, path);
    }
}
