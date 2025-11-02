using System.Collections.Generic;
using UnityEngine;

public class TowerTriggerZone : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private TowerAttak _towerAttak;
    [Tooltip("Список текущих целей башни")]
    [SerializeField] private List<GameObject> _targetsList = new List<GameObject>();
  

    private void FixedUpdate()
    {
        SearchMissingObject();
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.TryGetComponent(out EnemyMovement enemy))
         {
            _targetsList.Add(enemy.gameObject);
            _towerAttak.SetTargetList(_targetsList);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement enemy))
        {
            _targetsList.Remove(enemy.gameObject);
            _towerAttak.SetTargetList(_targetsList);
        }
    }

    private void SearchMissingObject()
    {
        _targetsList.RemoveAll(t => t.gameObject == null);
    }
}
