using UnityEngine;

public class MouseWorldAdapter
{
    private readonly Camera _camera;

    public MouseWorldAdapter(Camera camera) => _camera = camera;

    public Vector2 ScreenToWorld(Vector2 screenPos) => _camera.ScreenToWorldPoint(screenPos);
}
