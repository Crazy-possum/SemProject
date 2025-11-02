using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAOEBulletTriggerZone : MonoBehaviour
{
    public List<EnemyParametrs> TargetInAOE = new List<EnemyParametrs>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            TargetInAOE.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            TargetInAOE.Remove(enemy);
        }
    }
}
