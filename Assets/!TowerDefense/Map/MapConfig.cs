using System.Collections.Generic;
using UnityEngine;

public readonly struct MapConfig
{
    public Vector2Int GridSize { get; }
    public float CellSize { get; }
    public List<LayerMapConfig> Layers { get; }

    public MapConfig(Vector2Int mapSize,float cellSize, List<LayerMapConfig> layers)
    {
        GridSize = mapSize;
        CellSize = cellSize;
        Layers = layers;
    }
}
