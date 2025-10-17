using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public EnemyType Type { get; private set; }
    [field: SerializeField, Min(1f)] public float HP { get; private set; } = 1f;
    [field: SerializeField, Range(0.1f, 20f)] public float Speed { get; private set; }
    [field: SerializeField, Min(0f)] public float Damage { get; private set; }
}
