using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("Скорость врагов")]
    [SerializeField] private int _speed;
    [Tooltip("Погрешность пересечения с точками маршрута")]
    [SerializeField] private float _tolerance;

    private static Action _onEnemyEnter;
    private Transform[] _enemyWayPintsList;
    private Rigidbody _rb;
    private Vector3 _wayPointVector;
    private int _currentIndex;
    private float _checkDistance;

    public Transform[] EnemyWayPintsList { get => _enemyWayPintsList; set => _enemyWayPintsList = value; }
    public static Action OnEnemyEnter { get => _onEnemyEnter; set => _onEnemyEnter = value; }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentIndex = 0;
        _checkDistance = _speed * _tolerance;
    }

    void FixedUpdate()
    {
        SearchWayPoint();
        CheckExitEnter(); 

        if (_currentIndex == 0)
        {
            _wayPointVector = (_enemyWayPintsList[_currentIndex].position - transform.position).normalized;
        }
        else
        {
            _wayPointVector = (_enemyWayPintsList[_currentIndex].position - _enemyWayPintsList[_currentIndex - 1].position).normalized;
        }
        _rb.velocity = _wayPointVector * (_speed * Time.deltaTime);
    }

    private void SearchWayPoint()
    {
        if (Vector3.Distance(gameObject.transform.position, _enemyWayPintsList[_currentIndex].position) < _checkDistance)
        {
           _currentIndex += 1;
        }
    }

    private void CheckExitEnter()
    {
        int lastObject = _enemyWayPintsList.Length - 1;
        if (Vector3.Distance(gameObject.transform.position, _enemyWayPintsList[lastObject].position) < _checkDistance)
        {
            _onEnemyEnter?.Invoke();
            Destroy(gameObject);
        }
    }
}
