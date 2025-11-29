using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyParametrs _enemyParametrs;
    [Tooltip("Погрешность пересечения с точками маршрута")]
    [SerializeField] private float _tolerance;

    private static Action _onEnemyEnter;
    private Transform[] _enemyWayPintsList;
    private Rigidbody _rb;
    private Timer _slowingDownTimer;

    private Vector3 _wayPointVector;
    private int _currentIndex;
    private float _speed;
    private float _baseSpeed;
    private float _checkDistance;
    private bool _isAlreadySlowing;

    private GameObject _enemyTarget;
    private bool _isSlowOnce;
    private float _slowingDownValue;

    public Transform[] EnemyWayPintsList { get => _enemyWayPintsList; set => _enemyWayPintsList = value; }
    public static Action OnEnemyEnter { get => _onEnemyEnter; set => _onEnemyEnter = value; }
    public Timer SlowingDownTimer { get => _slowingDownTimer; set => _slowingDownTimer = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public bool IsAlreadySlowing { get => _isAlreadySlowing; set => _isAlreadySlowing = value; }

    void Start()
    {
        _speed = _enemyParametrs.EnemySO.Speed;

        _rb = GetComponent<Rigidbody>();
        _currentIndex = 0;
        _checkDistance = _speed * _tolerance;
        _baseSpeed = _speed;
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

        if (_slowingDownTimer != null & !_isSlowOnce)
        {
            SlowingCountdown(_enemyTarget, _slowingDownTimer, _slowingDownValue);
        }
    }

    private void OnEnable()
    {
        CharacterUpgrader.OnSlowMobsMove += UpdateMoveSpeed;
        CharacterBulletBehavior.OnHitEnemy += SlowingDownOnHit;
    }

    private void OnDisable()
    {
        CharacterUpgrader.OnSlowMobsMove -= UpdateMoveSpeed;
        CharacterBulletBehavior.OnHitEnemy -= SlowingDownOnHit;
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

    private void UpdateMoveSpeed(float slowingDown)
    {
        _speed = _speed - (_baseSpeed - (_baseSpeed * slowingDown));
    }

    private void SlowingDownOnHit(GameObject enemy, float slowingTimerValue, float slowingDownValue)
    {
        if (gameObject != enemy)
        {
            return;
        }

        if (_slowingDownTimer== null)
        {
            _slowingDownTimer = new Timer(slowingTimerValue);
        }

        if (!_isAlreadySlowing)
        {
            _speed = _speed - (_baseSpeed - (_baseSpeed * slowingDownValue));
            _isAlreadySlowing = true;

            if (_speed < 0)
            {
                _speed = 0;
            }
        }

        _enemyTarget = enemy;
        _slowingDownValue = slowingDownValue;

        _isSlowOnce = false;
        SlowingCountdown(_enemyTarget, _slowingDownTimer, _slowingDownValue);
    }

    private void SlowingCountdown(GameObject enemy, Timer slowTimer, float slowingDownValue)
    {
        slowTimer.Wait();

        if (!slowTimer.StartTimer)
        {
            slowTimer.StartCountdown();
        }

        if (slowTimer.ReachingTimerMaxValue == true)
        {
            slowTimer.StopCountdown();
            _isSlowOnce = true;
            _speed = _baseSpeed;
        }
    }
}
