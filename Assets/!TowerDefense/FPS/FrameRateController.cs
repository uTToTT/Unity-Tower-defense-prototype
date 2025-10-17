using UnityEngine;

public class FrameRateController : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate;

    [ContextMenu(nameof(ApplyFrameRate))]
    private void ApplyFrameRate() => Application.targetFrameRate = _targetFrameRate;
}
