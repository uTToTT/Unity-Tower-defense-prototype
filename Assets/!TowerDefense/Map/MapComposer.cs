using System.Collections.Generic;
using UnityEngine;

public class MapComposer
{
    private MapConfig _mapConfig;


    public MapConfig GetMap()
    {
        var layers = new List<LayerMapConfig>();

        var s_0 = new Vector2Int(1, 3);
        var f_0 = new Vector2Int(7, 5);
        var p_0 = "RRRRRRUUUULURDDDLLLLDRRRR";

        var layer_0 = new LayerMapConfig(s_0, f_0, p_0);

        var ms_0 = new Vector2Int(16, 9);
        var cs_0 = 1;

        layers.Add(layer_0);

        var map_0 = new MapConfig(ms_0,cs_0, layers);
        return map_0;

    }
}
