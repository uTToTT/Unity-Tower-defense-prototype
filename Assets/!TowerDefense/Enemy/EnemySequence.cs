public readonly struct EnemySequence
{
    public EnemyType EnemyType { get; }
    public int EnemyCount { get; }
    public float DelayBtwSpawn { get; }
    public float EnemyPowerModifier { get; }
    public float MoneyDropModifier { get; }

    public EnemySequence(
        EnemyType enemyType,
        int enemyCount,
        float delayBtwSpawn,
        float enemyPowerModifier,
        float moneyDropModifier)
    {
        EnemyType = enemyType;
        EnemyCount = enemyCount;
        DelayBtwSpawn = delayBtwSpawn;
        EnemyPowerModifier = enemyPowerModifier;
        MoneyDropModifier = moneyDropModifier;
    }
}
