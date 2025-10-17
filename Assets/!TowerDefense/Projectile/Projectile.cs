using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IMovable, IDisposable
{
    public event Action OnReachedTarget;

    private ITargetable _target;
    private Vector2 _targetLastPos;
    private float _speed;
    private float _damage;
    private bool _isTargetDead; 

    public void Init(ITargetable target, float damage, float speed)
    {
        Dispose(); 

        MoveManager.Instance.Register(this);

        _target = target;
        _speed = speed;
        _damage = damage;
        _isTargetDead = false;

        if (_target != null)
            _target.OnDeath += OnTargetDeath;

        if (_target?.Transform != null)
            _targetLastPos = _target.Transform.position;
    }

    public void Move()
    {
        if (IsTargetAlive())
        {
            _targetLastPos = _target.Transform.position;
        }

        transform.FollowTarget(_targetLastPos, _speed, 0);

        if (transform.IsReach(_targetLastPos))
        {
            if (IsTargetAlive())
                _target.TakeDamage(_damage);

            OnReachedTarget?.Invoke();
        }
    }

    private void OnTargetDeath(ITargetable target)
    {
        _isTargetDead = true;
        _target.OnDeath -= OnTargetDeath;
        _target = null;
    }

    private bool IsTargetAlive()
    {
        if (_isTargetDead || _target == null || _target.Equals(null))
            return false;

        return _target.IsAlive;
    }

    public void Dispose()
    {
        MoveManager.Instance.Unregister(this);

        if (_target != null)
            _target.OnDeath -= OnTargetDeath;

        _target = null;
        OnReachedTarget = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, _targetLastPos);
    }
}
