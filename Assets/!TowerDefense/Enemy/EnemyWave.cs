public readonly struct EnemyWave
{
    public EnemySequence[] EnemyGroups { get;  }

    public EnemyWave(EnemySequence[] enemyGroups)
    {
        EnemyGroups = new EnemySequence[enemyGroups.Length];
        enemyGroups.CopyTo(EnemyGroups, 0);
    }
}
