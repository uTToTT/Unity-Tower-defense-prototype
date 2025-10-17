using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFactory", menuName = "Enemy/Factory")]
public class EnemyFactory : FactoryBase<Enemy>
{
    public EnemyType EnemyType => Prefab.Type;
}
