using System.Collections.Generic;
using UnityEngine;

public class TowerAOEBulletTriggerZone : MonoBehaviour
{
    public List<EnemyParametrs> TargetInAOE = new List<EnemyParametrs>();

    private TowerEnum _parentTowerEnum;
    private bool _upgrade;

    public TowerEnum ParentTowerEnum { get => _parentTowerEnum; set => _parentTowerEnum = value; }
    public bool Upgrade { get => _upgrade; set => _upgrade = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            TargetInAOE.Add(enemy);
            if (_parentTowerEnum == TowerEnum.Catapult && _upgrade)
            {
                enemy.HasDamageWeekness = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            TargetInAOE.Remove(enemy);
            if (_parentTowerEnum == TowerEnum.Catapult && _upgrade)
            {
                enemy.HasDamageWeekness = false;
            }
        }
    }
}
