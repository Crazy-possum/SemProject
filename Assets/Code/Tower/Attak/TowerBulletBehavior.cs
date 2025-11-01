using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TowerBulletBehavior : MonoBehaviour
{
    [Tooltip("�������� ������ �������")]
    [SerializeField] private float _speed = 100f;

    [Tooltip("������� �������� ���� ��� 1 �������� �����������")]
    [SerializeField] private float _firstPaintingStage = 0.25f;
    [Tooltip("������� �������� ���� ��� 2 ��������� �����������")]
    [SerializeField] private float _secondPaintingStage = 0.5f;
    [Tooltip("������� �������� ���� ��� 3 ��������� �����������")]
    [SerializeField] private float _thirdPaintingStage = 0.75f;
    [Tooltip("������� �������� ���� ��� 4 ��������� �����������")]
    [SerializeField] private float _fourthPaintingStage = 1f;

    [SerializeField] private GameObject _aoeBullet;
    [SerializeField] private GameObject _singleBulletSprite;

    private TowerAOEBulletTriggerZone _bulletTriggerZone;
    private TowerScriptable _towerSO;
    private EnemyParametrs _currentEnemyHealth;
    private GameObject _bulletsCurrentTarget;
    private Transform _startBulletPosition;
    private Rigidbody _bulletRB;
    private Timer _timerDOTDuration;
    private Timer _timerDOTSpace;

    private TowerEnum _towerEnum;
    private Vector3 _movement;

    private bool _isStatic;
    private float _tolerance = 0.5f;

    private float _duration;
    private float _timeDOT;
    private int _maxDistance;
    private float _damage;

    public GameObject BulletsCurrentTarget { get => _bulletsCurrentTarget; set => _bulletsCurrentTarget = value; }
    public TowerScriptable TowerSO { get => _towerSO; set => _towerSO = value; }
    public Transform StartBulletPosition { get => _startBulletPosition; set => _startBulletPosition = value; }

    private void Start()
    {
        _currentEnemyHealth = _bulletsCurrentTarget.GetComponent<EnemyParametrs>();
        _bulletRB = GetComponent<Rigidbody>();

        _towerEnum = _towerSO.TowerEnum;
        _maxDistance = _towerSO.MaxBulletDistance;
        _duration = _towerSO.BulletDuration;
        _timeDOT = _towerSO.BulletDOTTime;
        _damage = _towerSO.TowerDamage;

        _timerDOTDuration = new Timer(_duration);
        _timerDOTSpace = new Timer(_timeDOT);

        if (_towerEnum != TowerEnum.Cannon && _towerEnum != TowerEnum.Shotgun)
        {
            SetDirectionToTargetOnce();
        }
        else if (_towerEnum == TowerEnum.Shotgun)
        {
            GetSpreadDirection();
        }
    }

    private void FixedUpdate()
    {
        if (_towerEnum == TowerEnum.Cannon)
        {
            SetDirectionToTarget();
        }

        CheckDistance();

        if (!_isStatic)
        {
            if (_towerEnum == TowerEnum.Catapult && Vector3.Distance(gameObject.transform.position, _bulletsCurrentTarget.transform.position) < _tolerance)
            {
                _isStatic = true;
                _bulletRB.velocity = Vector3.zero;
                _timerDOTDuration.StartCountdown();
                SwitchToAOE();
                _bulletTriggerZone = gameObject.GetComponentInChildren<TowerAOEBulletTriggerZone>();
                Debug.Log(_bulletTriggerZone);
            }
            else
            {
                _bulletRB.velocity = _movement;
            }
        }
        else
        {
            DamageOverTime();
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {

            if (_towerEnum != TowerEnum.Catapult)
            {
                DealDamage(enemy);
            }

            if (_towerEnum != TowerEnum.Sniper && _towerEnum != TowerEnum.Catapult)
            {
                Destroy(gameObject);
            }
        }
    }

    private void DealDamage(EnemyParametrs enemy)
    {
        float colorValue = enemy.CurrentPaintValue;
        float currentHealth = _currentEnemyHealth.CurrentHealth;

        switch (colorValue)
        {
            case 0: currentHealth -= 0; break;
            case 1: currentHealth = currentHealth - (_damage * _firstPaintingStage); break;
            case 2: currentHealth = currentHealth - (_damage * _secondPaintingStage); break;
            case 3: currentHealth = currentHealth - (_damage * _thirdPaintingStage); break;
            case 4: currentHealth = currentHealth - (_damage * _fourthPaintingStage); break;
        }

        _currentEnemyHealth.CurrentHealth = currentHealth;
    }

    private void DealAOEDamage(List<EnemyParametrs> targetList)
    {
        foreach (EnemyParametrs enemy in targetList)
        {

            float colorValue = enemy.CurrentPaintValue;
            float currentHealth = enemy.CurrentHealth;

            CountDamage(colorValue, currentHealth);
            enemy.CurrentHealth = currentHealth;
        }
    }

    private void CountDamage(float colorValue, float currentHealth)
    {
        float distanceFine = 0;

        if (_towerSO.TowerEnum == TowerEnum.Shotgun)
        {
            float distance = Vector3.Distance(_startBulletPosition.position, gameObject.transform.position);
            float distanceAfterNearestPoint = distance - 0.95f;

            if (distanceAfterNearestPoint < 0)
            {
                distance = 0;
            }

            distanceFine = distance * (100 / (_maxDistance));
        }

        float damageWithFin = _damage * (1 - (distanceFine / 100));

        switch (colorValue)
        {
            case 0: currentHealth -= 0; break;
            case 1: currentHealth = currentHealth - (damageWithFin * _firstPaintingStage); break;
            case 2: currentHealth = currentHealth - (damageWithFin * _secondPaintingStage); break;
            case 3: currentHealth = currentHealth - (damageWithFin * _thirdPaintingStage); break;
            case 4: currentHealth = currentHealth - (damageWithFin * _fourthPaintingStage); break;
        }
    }

    private void SetDirectionToTarget()
    {
        Vector3 move = (_bulletsCurrentTarget.transform.position - gameObject.transform.position).normalized;
        _movement.Set(_speed * move.x, _speed * move.y, 0);
    }

    private void SetDirectionToTargetOnce()
    {
        SetDirectionToTarget();
    }

    private void GetSpreadDirection()
    {
        Vector3 baseMoveVector = (_bulletsCurrentTarget.transform.position - gameObject.transform.position).normalized;

        Quaternion spreadRotation = Quaternion.Euler
            (0, 0, Random.Range(-_towerSO.AttakeAngle  / 2, _towerSO.AttakeAngle / 2));

        Vector3 move = spreadRotation * baseMoveVector;
        _movement.Set(_speed * move.x, _speed * move.y, 0);
    }

    private void SwitchToAOE()
    {
        _aoeBullet.SetActive(true);
        _singleBulletSprite.SetActive(false);
    }

    private void DamageOverTime()
    {
        Debug.Log(_bulletTriggerZone.TargetInAOE);
        List<EnemyParametrs> targetList = _bulletTriggerZone.TargetInAOE;
        Debug.Log(targetList);

        _timerDOTDuration.Wait();

        if (_timerDOTDuration.ReachingTimerMaxValue != true)
        {
            _timerDOTSpace.Wait();
            if (!_timerDOTSpace.StartTimer)
            {
                _timerDOTSpace.StartCountdown();
            }

            if (_timerDOTSpace.ReachingTimerMaxValue == true)
            {
                _timerDOTSpace.StopCountdown();
                DealAOEDamage(targetList);
            }
        }
        else
        {
            _timerDOTDuration.StopCountdown();
            Destroy(gameObject);
        }
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(_startBulletPosition.position, gameObject.transform.position);
        if (distance >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }
}