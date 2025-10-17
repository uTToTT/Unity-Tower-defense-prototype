using System;
using UnityEngine;

public interface IInputHandler : IDisposable
{
    event Action<Vector2> OnMouseMoved;
    event Action<Vector2> OnLMBClick;
    event Action<Vector2> OnRMBClick;

    event Action<Vector2> OnMouseMovedWorld;
    event Action<Vector2> OnLMBClickWorld;
    event Action<Vector2> OnRMBClickWorld;

    event Action OnSpacePressed;

    void EnableInput();
    void DisableInput();

    void SwitchToGameplay();
    void SwitchToUI();
}
