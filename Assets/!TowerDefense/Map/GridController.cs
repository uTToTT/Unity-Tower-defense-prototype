using System;
using UnityEngine;

public class GridController
{
    private float _cellSize;

    private Vector2Int _gridSize;
    private Vector3 _gridOrigin;

    public Vector3 GridOrigin => _gridOrigin;
    public float CellSize => _cellSize;

    public GridController(Vector2Int gridSize, float cellSize)
    {
        _gridSize = gridSize;
        _cellSize = cellSize;

        RecalculateOrigin();
    }

    public void DrawGizmos()
    {
        Vector3 gridCenter = new Vector3(0f, 0f, 0f);
        Vector3 gridSizeWorld = new Vector3(_gridSize.x * _cellSize, _gridSize.y * _cellSize, 0f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gridCenter, gridSizeWorld);
    }

    public Vector2Int WorldToCell(Vector3 worldPos)
    {
        Vector3 localPos = worldPos - _gridOrigin;

        int x = Mathf.FloorToInt(localPos.x / _cellSize);
        int y = Mathf.FloorToInt(localPos.y / _cellSize);

        return new Vector2Int(x, y);
    }

    public Vector3 CellToWorld(Vector2Int cellPos)
    {
        float worldX = _gridOrigin.x + (cellPos.x + 0.5f) * _cellSize;
        float worldY = _gridOrigin.y + (cellPos.y + 0.5f) * _cellSize;

        return new Vector3(worldX, worldY, 0f);
    }

    public Vector3 GetCellCenterFromWorld(Vector3 worldPos)
    {
        Vector2Int cell = WorldToCell(worldPos);
        return CellToWorld(cell);
    }

    public bool IsCellInsideGrid(Vector2Int cell)
    {
        return
            cell.x >= 0 &&
            cell.y >= 0 &&
            cell.x < _gridSize.x &&
            cell.y < _gridSize.y;
    }

    public bool IsWorldPositionInsideGrid(Vector3 worldPos)
    {
        Vector2Int cell = WorldToCell(worldPos);
        return IsCellInsideGrid(cell);
    }

    private void RecalculateOrigin()
    {
        _gridOrigin = new Vector3(
            -_gridSize.x * 0.5f * _cellSize,
            -_gridSize.y * 0.5f * _cellSize,
            0f);
    }
}
