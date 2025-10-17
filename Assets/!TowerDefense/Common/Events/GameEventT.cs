using System;
using System.Collections.Generic;

public class GameEventT<T> : GameEventBase
{
    private readonly List<Action<T>> _listeners = new();

    public void Raise(T value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i]?.Invoke(value);
        }
    }

    public void Register(Action<T> listener)
    {
        if (!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    public void Unregister(Action<T> listener)
    {
        _listeners.Remove(listener);
    }
}
