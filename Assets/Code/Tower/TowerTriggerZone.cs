using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTriggerZone : MonoBehaviour
{
    [SerializeField] private TowerAttak _towerAttak;

    [SerializeField] private List<GameObject> _targetsList = new List<GameObject>();
  
    private GameObject _currentTarget;
    private bool _haveTarget;

    private void Start()
    {
        //_currentTarget = null;
    }

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
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.TryGetComponent(out EnemyMovement enemy))
         {
            _targetsList.Add(enemy.gameObject);
            _haveTarget = true;

            /**if (_haveTarget)
            {
                foreach (GameObject target in _targetsList)
                {
                    for (int i = 0; i < _targetsList.Count; i++)
                    {
                        if (_targetsList[i] == _targetsList[i + 1])
                        {
                            _targetsList.RemoveAt(i + 1);
                        }

                        if (_targetsList[i] == null)
                        {
                            _targetsList.RemoveAt(i);
                        }
                    }
                }
            }**/
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
}
