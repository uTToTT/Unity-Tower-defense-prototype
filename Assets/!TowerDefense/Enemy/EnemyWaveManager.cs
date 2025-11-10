using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour, IInitializable
{
    [SerializeField] private EnemyFactory[] _enemyFactories;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameEventFloat _onEnemyReachedFinish;

    private EnemySpawner _enemySpawner;

    public bool IsInitialized { get; private set; }

    private EnemyWave _wave;
    private bool _started;

    public bool Init()
    {
        _enemySpawner = new EnemySpawner(_enemyFactories, _spawnPoint.position, _onEnemyReachedFinish);

        IsInitialized = true;
        return IsInitialized;
    }

    [ContextMenu(nameof(StartWave))]
    private void StartWave()
    {
        _wave = LoadWave();
        _started = true;
    }

    private void Update()
    {
        if (!_started) return;
    }

    private EnemyWave LoadWave()
    {
        List<EnemySequence> groups = new();

        //EnemySequence eg_0 = new EnemySequence(EnemyType.Small, 10, 0.5f, 1, 1);
        //EnemySequence eg_1 = new EnemySequence(EnemyType.Medium, 15, 0.25f, 1, 1);

        EnemyWave wave = new EnemyWave(groups.ToArray());

        return wave;
    }
}
