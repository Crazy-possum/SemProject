using System;
using UnityEngine;

public class CharacterBulletBehavior : MonoBehaviour
{
    [Tooltip("Скорость полета снаряда")]
    [SerializeField] private float _speed = 100f;
    [Tooltip("Значение покраса от 1 попадания снаряда")]
    [SerializeField] private float _painting = 1;
    [Tooltip("Максимальная дистанция полета снаряда")]
    [SerializeField] private int _maxDistance;

    private static Action<GameObject, float, int> _onHitEnemy;

    private Transform _startBulletPosition;
    private Rigidbody _bulletRb;
    private Camera _camera;
    private Vector3 _movement;

    private bool _isSlowingMobsActive;
    private float _slowingTimerValue;
    private int _slowingDownValue;

    public Transform StartBulletPosition { get => _startBulletPosition; set => _startBulletPosition = value; }
    public static Action<GameObject, float, int> OnHitEnemy { get => _onHitEnemy; set => _onHitEnemy = value; }

    private void Start()
    {
        _bulletRb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        _bulletRb.velocity = _movement;
        MoveOnVector();
        CheckDistance();
    }

    private void OnEnable()
    {
        CharacterUpgrader.OnSlowDownMobs += IsSlowingMobsActive;
    }

    private void OnDisable()
    {
        CharacterUpgrader.OnSlowDownMobs -= IsSlowingMobsActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            EnemyParametrs enemyParametrs = enemy.GetComponent<EnemyParametrs>();

            if (enemyParametrs.CurrentPaintValue < 4)
            {
                enemyParametrs.CurrentPaintValue += _painting;
                Destroy(gameObject);
            }

            if (_isSlowingMobsActive)
            {
                _onHitEnemy?.Invoke(enemy.gameObject, _slowingTimerValue, _slowingDownValue);
            }
        }
    }

    private void MoveOnVector()
    {
        Vector3 direction = (_camera.ScreenToWorldPoint(Input.mousePosition) - _startBulletPosition.position).normalized;
        _movement.Set(direction.x * _speed, direction.y * _speed, 0);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(_startBulletPosition.position, gameObject.transform.position);
        if (distance >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void IsSlowingMobsActive(float debuffTimerValue, int slowingDown)
    {
        _isSlowingMobsActive = true;
        _slowingTimerValue = debuffTimerValue;
        _slowingDownValue = slowingDown;
    }
}
