using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpawner
{
    private readonly Dictionary<EnemyType, EnemyFactory> _enemyFactoryByType = new();
    private readonly GameEventFloat _onEnemyReachedFinish;

    private Vector3 _spawnPoint;

    public EnemySpawner(
        EnemyFactory[] enemyFactories,
        Vector3 spawnPoint,
        GameEventFloat onEnemyReachedFinish)
    {
        InitFactories(enemyFactories);

        _spawnPoint = spawnPoint;
        _onEnemyReachedFinish = onEnemyReachedFinish;
    }

    private void InitFactories(EnemyFactory[] enemyFactories)
    {
        foreach (var factory in enemyFactories)
        {
            if (factory != null) continue;
            if (_enemyFactoryByType.ContainsKey(factory.EnemyType) == true)
            {
                Debug.LogWarning("Same factories already exists");
                continue;
            }

            _enemyFactoryByType[factory.EnemyType] = factory;
            factory.Init();
        }
    }

    private void OnEnemyFinishReached(Enemy enemy)
    {
        ReturnEnemy(enemy);
        _onEnemyReachedFinish.Raise(enemy.Damage);
    }

    private void OnEnemyDie(ITargetable enemy)
    {
        if (enemy is Enemy enemy1)
            ReturnEnemy(enemy1);
    }

    private void ReturnEnemy(Enemy enemy)
    {
        enemy.PathEnd -= OnEnemyFinishReached;
        enemy.OnDeath -= OnEnemyDie;

        _enemyFactoryByType[enemy.Type].Return(enemy);
    }

    public void SpawnEnemy(EnemyType type)
    {
        var enemy = _enemyFactoryByType[type].Create();
        enemy.transform.position = _spawnPoint;

        enemy.PathEnd += OnEnemyFinishReached;
        enemy.OnDeath += OnEnemyDie;
    }
}
