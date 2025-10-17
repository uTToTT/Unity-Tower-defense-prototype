using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IPoolable, IEntityLifecycle
{
    // AttackModule
    // ResourceModule

    [SerializeField] private float _damage;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _delayAttack;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private TowerType _type;
    [SerializeField] private Projectile _bulletPrefab;

    private float _elapsed;
    private bool _reloaded;
    private List<Enemy> _targets = new();

    public bool IsActive { get; set ; }
    public TowerType Type => _type;

    private void OnTriggerEnter2D(Collider2D collision) // tmp
    {
        if (collision.CompareTag(Constants.ENEMY_TAG))
        {
            var enemy = collision.GetComponent<Enemy>();
            _targets.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // tmp
    {
        if (collision.CompareTag(Constants.ENEMY_TAG))
        {
            var enemy = collision.GetComponent<Enemy>();
            _targets.Remove(enemy);
        }
    }

    private void Update()
    {

        if (!_reloaded)
        {
            _elapsed += Time.deltaTime;
            if (_elapsed >= _delayAttack)
            {
                _reloaded = true;
                _elapsed = 0;
            }
        }



        if (_targets.Count > 0)
        {
            if (_reloaded)
                Attack();
            transform.LookAt2D(_targets[0].transform);
        }
    }

    private void Attack()
    {
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _launchPoint.position;

        bullet.Init(_targets[0], _damage, _projectileSpeed);
        bullet.OnReachedTarget += () =>
        {
            bullet.Dispose();
            Destroy(bullet.gameObject);
        };

        _reloaded = false;
    }

    public void Dispose()
    {
        //throw new System.NotImplementedException();
    }

    public void OnPreload()
    {
        //throw new System.NotImplementedException();
    }

    public void OnActivated()
    {
        //throw new System.NotImplementedException();
    }

    public void OnDeactivated()
    {
        //throw new System.NotImplementedException();
    }

    public void OnReturned()
    {
        //throw new System.NotImplementedException();
    }

    public void OnDestroyed()
    {
        //throw new System.NotImplementedException();
    }
}
