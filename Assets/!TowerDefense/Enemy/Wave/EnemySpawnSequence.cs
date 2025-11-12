using System;
using UnityEngine;

[Serializable]
public class EnemySpawnSequence
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField, Range(1, 200)] private int _amount;
    [SerializeField, Range(0.1f, 10f)] private float _cooldown;

    public State Begin() => new State(this);

    [Serializable]
    public struct State
    {
        private int _count;
        private float _cooldown;

        private EnemySpawnSequence _sequence;

        public State(EnemySpawnSequence sequence)
        {
            _sequence = sequence;
            _count = 0;
            _cooldown = sequence._cooldown;
        }

        public float Progress(float deltaTime)
        {
            _cooldown += deltaTime;

            while (_cooldown >= _sequence._cooldown)
            {
                _cooldown -= _sequence._cooldown;
                if (_count >= _sequence._amount)
                {
                    return _cooldown;
                }
                _count += 1;
                Game.SpawnEnemy(_sequence._enemyFactory);
            }
            return -1f;
        }
    }
}
