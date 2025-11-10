using UnityEngine;
[CreateAssetMenu(fileName = "EnemyWave", menuName = "Enemy/EnemyWave")]
public class EnemyWave : ScriptableObject
{
    [SerializeField]
    private EnemySequence[] _enemySequences;

    public EnemyWave(EnemySequence[] enemyGroups)
    {
        _enemySequences = new EnemySequence[enemyGroups.Length];
        enemyGroups.CopyTo(_enemySequences, 0);
    }
}
