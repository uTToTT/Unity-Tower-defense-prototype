using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerInputHandler : IInputHandler
{
    public event Action<Vector2> OnMouseMoved;
    public event Action<Vector2> OnLMBClick;
    public event Action<Vector2> OnRMBClick;

    public event Action<Vector2> OnMouseMovedWorld;
    public event Action<Vector2> OnLMBClickWorld;
    public event Action<Vector2> OnRMBClickWorld;

    public event Action OnSpacePressed;

    private readonly GameInput _gameInput;
    private readonly MouseWorldAdapter _mouseAdapter;

    public PlayerInputHandler(MouseWorldAdapter mouseAdapter)
    {
        _gameInput = new GameInput();
        _mouseAdapter = mouseAdapter;
        SubOnEvents();
    }

    public void Dispose()
    {
        UnsubOnEvents();
        _gameInput.Dispose();
    }

    public void EnableInput() => _gameInput.Enable();
    public void DisableInput() => _gameInput.Disable();

    public void SwitchToGameplay()
    {
        _gameInput.UI.Disable();

        _gameInput.Gameplay.Enable();
    }

    public void SwitchToUI()
    {
        _gameInput.Gameplay.Disable();

        _gameInput.UI.Enable();
    }

    private void SubOnEvents()
    {
        _gameInput.Gameplay.MousePosition.performed += OnMousePositionPerformed;
        _gameInput.Gameplay.LMBClicked.performed += OnLMBPerformed;
        _gameInput.Gameplay.SpacePressed.performed += OnSpacePerformed;
    }

    private void UnsubOnEvents()
    {
        _gameInput.Gameplay.MousePosition.performed -= OnMousePositionPerformed;
        _gameInput.Gameplay.LMBClicked.performed -= OnLMBPerformed;
        _gameInput.Gameplay.SpacePressed.performed -= OnSpacePerformed;
    }

    private void OnSpacePerformed(InputAction.CallbackContext context) =>
        OnSpacePressed?.Invoke();

    private void OnLMBPerformed(InputAction.CallbackContext context)
    {
        var screenPointPos = _gameInput.Gameplay.MousePosition.ReadValue<Vector2>();
        var worldPointPos = _mouseAdapter.ScreenToWorld(screenPointPos);

        OnLMBClick?.Invoke(screenPointPos);
        OnLMBClickWorld?.Invoke(worldPointPos);
    }

    private void OnMousePositionPerformed(InputAction.CallbackContext context)
    {
        var screenPointPos = context.ReadValue<Vector2>();
        var worldPointPos = _mouseAdapter.ScreenToWorld(screenPointPos);

        OnMouseMoved?.Invoke(screenPointPos);
        OnMouseMovedWorld?.Invoke(worldPointPos);
    }
}
