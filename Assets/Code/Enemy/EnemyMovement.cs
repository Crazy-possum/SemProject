using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private float _tolerance;

    public Transform[] EnemyWayPintsList;

    public static event Action EnemyEnter;

    private Rigidbody _rb;
    private Vector3 _wayPointVector;
    private int _currentIndex;
    private float _checkDistanse;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentIndex = 0;
        _checkDistanse = _speed * _tolerance;
    }

    void FixedUpdate()
    {
        SearchWayPoint();
        CheckExitEnter(); 

        if (_currentIndex == 0)
        {
            _wayPointVector = (EnemyWayPintsList[_currentIndex].position - transform.position).normalized;
            _rb.velocity = _wayPointVector * _speed * Time.deltaTime;
        }
        else
        {
            _wayPointVector = (EnemyWayPintsList[_currentIndex].position - EnemyWayPintsList[_currentIndex - 1].position).normalized;
            _rb.velocity = _wayPointVector * _speed * Time.deltaTime;
        }
    }

    private void SearchWayPoint()
    {
        if (Vector3.Distance(gameObject.transform.position, EnemyWayPintsList[_currentIndex].position) < _checkDistanse)
        {
           _currentIndex += 1;
        }
    }

    private void CheckExitEnter()
    {
        int lastObject = EnemyWayPintsList.Length - 1;
        if (Vector3.Distance(gameObject.transform.position, EnemyWayPintsList[lastObject].position) < _checkDistanse)
        {
            EnemyEnter?.Invoke();
            Destroy(gameObject);
        }
    }
}
