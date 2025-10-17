using System;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : MonoBehaviour, IDebuggable
{
    [Serializable]
    private class InitializationGroup
    {
        [SerializeField] private string _name;
        [SerializeField] private MonoBehaviour[] _targets;

        #region ==== Properties ====

        public string Name => _name;
        public IEnumerable<MonoBehaviour> Targets => _targets;

        #endregion =================
    }

    [Header("Debug")]
    [SerializeField] private bool _debugging;

    [Header("InitGroups")]
    [SerializeField] private InitializationGroup[] _awakeGroups;

    #region ==== Properties ====

    public bool Debugging => _debugging;

    #endregion =================

    #region ==== Unity API ====

    private void Awake() => InitGroups(_awakeGroups);
    private void OnValidate() => ValidateGroups(_awakeGroups);

    #endregion ================

    #region ==== Debug ====

    public void SetDebug(bool state) => _debugging = state;

    #endregion ============

    #region ==== Init ====

    private void InitGroups(InitializationGroup[] groups)
    {
        foreach (var group in groups)
        {
            foreach (var target in group.Targets)
            {
                if (target && target is IInitializable initTarget)
                {
                    if (initTarget.Init())
                    {
                        DebugUtils.LogIfDebug
                            (this, $"[{group.Name}][{target.name}] Init success");
                    }
                    else
                    {
                        Debug.LogError($"[{group.Name}][{target.name}] Init failed");
                    }
                }
            }
        }
    }

    #endregion ===========

    #region ==== Validation ====

    private void ValidateGroups(InitializationGroup[] groups)
    {
        foreach (var group in groups)
        {
            foreach (var target in group.Targets)
            {
                if (target && target is not IInitializable)
                {
                    Debug.LogError
                        ($"Target [{target.name}] in group {group.Name} must implement IInitializable.", target);
                }
            }
        }
    }

    #endregion =================
}
