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
    private Timer _slowingDownTimer;

    private Vector3 _wayPointVector;
    private int _currentIndex;
    private int _baseSpeed;
    private float _checkDistance;
    private bool _isAlreadySlowing;

    public Transform[] EnemyWayPintsList { get => _enemyWayPintsList; set => _enemyWayPintsList = value; }
    public static Action OnEnemyEnter { get => _onEnemyEnter; set => _onEnemyEnter = value; }
    public Timer SlowingDownTimer { get => _slowingDownTimer; set => _slowingDownTimer = value; }
    public int Speed { get => _speed; set => _speed = value; }
    public bool IsAlreadySlowing { get => _isAlreadySlowing; set => _isAlreadySlowing = value; }

    void Start()
    {
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

    private void UpdateMoveSpeed(int slowingDown)
    {
        _speed = _speed - (_baseSpeed - (_baseSpeed * slowingDown));
    }

    private void SlowingDownOnHit(GameObject enemy, float slowingTimerValue, int slowingDownValue)
    {
        Timer slowTimer = enemy.GetComponent<EnemyMovement>().SlowingDownTimer;
        slowTimer = new Timer(slowingTimerValue);

        bool isSlow = enemy.GetComponent<EnemyMovement>().IsAlreadySlowing;
        if (!isSlow)
        {
            _speed = _speed - (_baseSpeed - (_baseSpeed * slowingDownValue));
        }

        SlowingCountdown(enemy, slowTimer, slowingDownValue);
    }

    private void SlowingCountdown(GameObject enemy, Timer slowTimer, int slowingDownValue)
    {
        slowTimer.Wait();

        if (!slowTimer.StartTimer)
        {
            slowTimer.StartCountdown();
        }

        if (slowTimer.ReachingTimerMaxValue == true)
        {
            slowTimer.StopCountdown();
            _speed = _speed + (_baseSpeed - (_baseSpeed * slowingDownValue));
        }
    }
}
