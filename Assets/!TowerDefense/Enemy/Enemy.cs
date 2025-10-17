using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovable, IPoolable, IEntityLifecycle, ITargetable
{
    public event Action<ITargetable> OnDeath;
    public event Action<Enemy> PathEnd;

    [SerializeField] private EnemyConfig _config;

    private PathController _pathController;
    private HPModule _hpModule;

    public bool IsActive { get; set; }
    public EnemyType Type => _config.Type;
    public float Damage => _config.Damage;

    public Transform Transform => transform;

    public bool IsAlive => _hpModule.IsAlive();

    #region ==== Init ====

    public void Dispose()
    {
        OnDeath = null;
        PathEnd = null;

        _hpModule.OnDie -= Die;
        Destroy(gameObject);
    }

    #endregion ===========

    #region ==== Lifecycle ====

    public void OnPreload()
    {
        _pathController = new PathController();
        _hpModule = new HPModule();
        _hpModule.OnDie += Die;
    }

    public void OnActivated()
    {
        UnsubEvents();

        MoveManager.Instance.Register(this);
        var path = MoveManager.Instance.GetPath();
        _pathController.Clear();
        _hpModule.SetMaxHP(_config.HP); // tmp

        foreach (var point in path) // tmp
        {
            _pathController.Enqueue(new Vector3(point.x, point.y, 0));
        }
    }

    public void OnDeactivated()
    {
        MoveManager.Instance.Unregister(this);
    }

    public void OnReturned()
    {
        UnsubEvents();
    }

    public void OnDestroyed()
    {
        UnsubEvents();
    }

    #endregion

    public void TakeDamage(float damageAmount) => _hpModule.TakeDamage(damageAmount);
    private void Die() => OnDeath?.Invoke(this);

    public void Move()
    {
        if (!_pathController.HasPath)
        {
            PathEnd?.Invoke(this);
            MoveManager.Instance.Unregister(this);
            return;
        }

        Vector3 target = _pathController.Peek();

        transform.MoveTowards(target, _config.Speed);

        if (transform.IsReach(target))
        {
            _pathController.Dequeue();
            //_pathController.Enqueue(target);
        }
    }

    private void UnsubEvents()
    {
        OnDeath = null;
        PathEnd = null;
    }
}
