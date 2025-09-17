using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTriggerZone : MonoBehaviour
{
    [SerializeField] private TowerAttak _towerAttak;
  
    private GameObject _currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyPatrol enemy))
        {
            _currentTarget = other.gameObject;
            _towerAttak.CurrentTarget = _currentTarget;
        }
    }
}
