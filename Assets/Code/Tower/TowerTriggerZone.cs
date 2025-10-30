using System.Collections.Generic;
using UnityEngine;

public class TowerTriggerZone : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private TowerAttak _towerAttak;
    [Tooltip("Список текущих целей башни")]
    [SerializeField] private List<GameObject> _targetsList = new List<GameObject>();
  
    private GameObject _currentTarget;
    private bool _haveTarget;

    private void FixedUpdate()
    {
        //TODO Перенести логику в другой скрипт
        if (_haveTarget)
        {
            if (_currentTarget == null && _targetsList.Count > 0)
            {
                _currentTarget = _targetsList[0];
                _towerAttak.CurrentTarget = _currentTarget;
            }
        }

        if (_targetsList == null)
        {
            _haveTarget = false;
        }

        SearchMissingObject();
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.TryGetComponent(out EnemyMovement enemy))
         {
            _targetsList.Add(enemy.gameObject);
            _haveTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement enemy))
        {
            _targetsList.Remove(enemy.gameObject);
            _currentTarget = null;
            _towerAttak.CurrentTarget = null;
        }
    }

    private void SearchMissingObject()
    {
        _targetsList.RemoveAll(t => t.gameObject == null);
    }
}
