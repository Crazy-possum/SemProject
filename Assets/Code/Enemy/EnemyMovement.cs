using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int _speed;

    public Transform[] EnemyWayPintsList;

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
        float tolerance = 0.03f;
        if (Vector3.Distance(gameObject.transform.position, EnemyWayPintsList[_currentIndex].position) < tolerance)
        {
           _currentIndex += 1;
           //_currentIndex = _currentIndex + 1;
        }
    }
}
