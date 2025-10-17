using System;
using UnityEngine;

public class CellSelector : IDisposable
{
    public event Action<Vector3> ClickCellWorld;
    public event Action<Vector2Int> ClickCell;

    private GridController _gridController;

    private float _cellSize;

    private Vector3 _gridOrigin;
    private Vector2Int _currCell;

    public CellSelector(GridController gridController)
    {
        _gridController = gridController;

        _cellSize = _gridController.CellSize;
        _gridOrigin = _gridController.GridOrigin;

        InputManager.Instance.Current.OnLMBClickWorld += OnClick;
    }

    public void DrawGizmos()
    {
        if (!Application.isPlaying) return;

        if (_gridController.IsCellInsideGrid(_currCell))
        {
            Vector3 cellCenter = _gridOrigin + new Vector3(
                (_currCell.x + 0.5f) * _cellSize,
                (_currCell.y + 0.5f) * _cellSize,
                0f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(cellCenter, Vector3.one * _cellSize);
        }
    }

    private void OnClick(Vector2 mousePos) 
    {
        _currCell = _gridController.WorldToCell(mousePos);

        if (_gridController.IsCellInsideGrid(_currCell))
        {
            ClickCell?.Invoke(_currCell);
            ClickCellWorld?.Invoke(_gridController.CellToWorld(_currCell));
        }
    }

    public void Dispose()
    {
        InputManager.Instance.Current.OnLMBClickWorld -= OnClick;
    }
}
