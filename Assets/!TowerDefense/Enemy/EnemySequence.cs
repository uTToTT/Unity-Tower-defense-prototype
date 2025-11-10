using System;
using UnityEngine;

[Serializable]
public class EnemySequence
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private EnemyType _enemyType => _enemyFactory.EnemyType;
    [SerializeField, Range(1, 200)] private int _amount;
    [SerializeField, Range(0.1f, 10f)] private float _cooldown;
}
