using System;
using UnityEngine;

public class InputManager : MonoBehaviour, IInitializable
{
    public event Action<CellSelector> OnCellSelectorInit;

    [SerializeField] private Camera _camera;

    public static InputManager Instance { get; private set; }
    public bool IsInitialized { get; private set; }

    public IInputHandler Current { get; private set; }
    public CellSelector CellSelector { get; private set; } // Should be changed to ICellSelector

    public bool Init()
    {
        Instance = this;

        var mouseAdapter = new MouseWorldAdapter(_camera);
        SetHandler(new PlayerInputHandler(mouseAdapter));
        Current.SwitchToGameplay();

        IsInitialized = true;
        return IsInitialized;
    }

    public void InitCellSelector(GridController gridController)
    {
        CellSelector?.Dispose();
        CellSelector = new CellSelector(gridController);
        OnCellSelectorInit?.Invoke(this.CellSelector);
    }

    public void SetHandler(IInputHandler input)
    {
        Current?.DisableInput();
        Current?.Dispose();
        Current = input;
        Current.EnableInput();
    }

    private void OnDrawGizmosSelected()
    {
        if (CellSelector == null) return;

        CellSelector.DrawGizmos();
    }
}
