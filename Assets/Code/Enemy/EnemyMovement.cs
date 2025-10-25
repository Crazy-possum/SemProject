using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int _speed;

    public Transform[] EnemyWayPintsList;

    public static event Action EnemyEnter;

    private Rigidbody _rb;
    private Vector3 _wayPointVector;
    private int _currentIndex;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentIndex = 0;
    }

    private void Update()
    {
        SearchWayPoint();
        CheckExitEnter();
    }

    void FixedUpdate()
    {
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
        float tolerance = 0.04f;
        if (Vector3.Distance(gameObject.transform.position, EnemyWayPintsList[_currentIndex].position) < tolerance)
        {
           _currentIndex += 1;
        }
    }

    private void CheckExitEnter()
    {
        float tolerance = 0.05f;
        int lastObject = EnemyWayPintsList.Length - 1;
        if (Vector3.Distance(gameObject.transform.position, EnemyWayPintsList[lastObject].position) < tolerance)
        {
            EnemyEnter?.Invoke();
            Destroy(gameObject);
        }
    }
}
