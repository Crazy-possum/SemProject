using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTriggerZone : MonoBehaviour
{
    [SerializeField] private TowerAttak _towerAttak;

    [SerializeField] private List<GameObject> _targetsList = new List<GameObject>();
  
    private GameObject _currentTarget;
    private bool _haveTarget;

    private void Update()
    {
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
            _towerAttak.CurrentTarget = _currentTarget;
        }
    }

    private void SearchMissingObject()
    {
        foreach (GameObject target in _targetsList)
        {
            for (int i = 0; i < _targetsList.Count; i++)
            {
                if (_targetsList[i] == null)
                {
                    Destroy(_targetsList[i]);
                    _targetsList.RemoveAt(i);
                }
            }
        }
    }
}
