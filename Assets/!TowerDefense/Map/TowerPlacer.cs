using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerPlacer : MonoBehaviour, IInitializable
{
    [SerializeField] private TowerFactory[] _factories;
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private Transform _towerParent;

    private readonly Dictionary<TowerType, TowerFactory> _factoryByType = new();

    public bool IsInitialized { get; private set; }

    #region ==== Init ====

    public bool Init()
    {
        foreach (var factory in _factories)
        {
            factory.Init();
            _factoryByType.Add(factory.TowerType, factory);
        }

        InputManager.Instance.CellSelector.ClickCellWorld += Place;

        IsInitialized = true;
        return IsInitialized;
    }

    

    #endregion ===========

    #region ==== Unity API ====

    private void OnValidate()
    {
        ValidateFactoryDuplicates();
    }

    #endregion

    private void Place(Vector3 pos)
    {

        Place(TowerType.Tower_0, pos);
    }

    public void Place(TowerType towerType, Vector3 pos)
    {
        if (_mapManager.GridController.IsWorldPositionInsideGrid(pos) == false) return;

        if (_factoryByType.TryGetValue(towerType, out var factory))
        {
            var tower = factory.Create();

            tower.transform.position = (pos);
            tower.transform.SetParent(_towerParent);
        }
    }

    #region ==== Validation ====

    private void ValidateFactoryDuplicates()
    {
        if (_factories == null) return;
        if (_factories.Length == 0) return;

        var duplicateTypes = _factories
            .Where(f => f != null)
            .GroupBy(f => f.TowerType)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateTypes.Count > 0)
        {
            var duplicates = string.Join(", ", duplicateTypes);
            Debug.LogWarning($"[{nameof(TowerPlacer)}] Find duplicates: {duplicates}");
        }
    }

    #endregion
}
