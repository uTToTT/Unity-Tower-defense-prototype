using System.Collections.Generic;
using UnityEngine;

public class PathController
{
    private readonly Queue<Vector3> _waypoints = new();

    #region ==== Properties ====

    public bool HasPath => _waypoints.Count > 0;

    #endregion =================

    public void Enqueue(Vector3 point) => _waypoints.Enqueue(point);
    public Vector3 Peek() => _waypoints.Peek();

    public void Dequeue()
    {
        if (_waypoints.Count > 0)
            _waypoints.Dequeue();
    }

    public void Clear() => _waypoints.Clear();
}
