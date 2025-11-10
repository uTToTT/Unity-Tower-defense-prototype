using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "GameScenario", menuName = "Game/GameScenario")]
public class GameScenario : ScriptableObject
{
    [Expandable]
    [SerializeField] private EnemyWave[] _waves;
}
