using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, IInitializable
{
    [SerializeField] private bool _drawGizmos;

    private GridController _gridController;
    private MapComposer _mapComposer;

    private MapConfig _currMap;

    public bool IsInitialized { get; private set; }

    public GridController GridController => _gridController;

    public bool Init()
    {
        _mapComposer = new MapComposer();
        _currMap = LoadMap();

        _gridController = new GridController(_currMap.GridSize, _currMap.CellSize);

        InputManager.Instance.InitCellSelector(_gridController);

        IsInitialized = true;
        return true;
    }

    public List<Vector3> GetPath()
    {
        var path = new List<Vector3>();

        foreach (var step in _currMap.Layers[0].ComputedPath)
        {
            path.Add(_gridController.CellToWorld(step)); 
        }

        return path;
    }

    private MapConfig LoadMap()
    {
        return _mapComposer.GetMap();
    }

    private void OnDrawGizmosSelected()
    {
        if (!IsInitialized) return;
        if (!_drawGizmos) return;

        _gridController.DrawGizmos();
    }
}
