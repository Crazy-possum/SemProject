using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private EnemeyPattern _enemeyPattern;
    [Tooltip("Список текущих целей башни")]
    [SerializeField] private List<EnemyParametrs> _healTargetsList = new List<EnemyParametrs>();


    private void FixedUpdate()
    {
        SearchMissingObject();
        _enemeyPattern.HealTargetsList = _healTargetsList;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyParametrs enemy))
        {
            _healTargetsList.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyParametrs enemy))
        {
            _healTargetsList.Remove(enemy);
        }
    }

    private void SearchMissingObject()
    {
        _healTargetsList.RemoveAll(t => t.gameObject == null);
    }
}
