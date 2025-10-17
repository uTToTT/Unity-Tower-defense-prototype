using UnityEngine;

[CreateAssetMenu(fileName = "TowerFactory", menuName = "Tower/Factory")]
public class TowerFactory : FactoryBase<Tower>
{
    public TowerType TowerType => Prefab.Type;
}
