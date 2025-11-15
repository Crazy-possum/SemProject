using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TowerBulletBehavior : MonoBehaviour
{
    [Tooltip("Скорость полета снаряда")]
    [SerializeField] private float _speed = 100f;

    [Tooltip("Процент пробития моба при 1 единичке окрашивания")]
    [SerializeField] private float _firstPaintingStage = 0.25f;
    [Tooltip("Процент пробития моба при 2 единичках окрашивания")]
    [SerializeField] private float _secondPaintingStage = 0.5f;
    [Tooltip("Процент пробития моба при 3 единичках окрашивания")]
    [SerializeField] private float _thirdPaintingStage = 0.75f;
    [Tooltip("Процент пробития моба при 4 единичках окрашивания")]
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
    private Timer _timerUpgradeDOT;
    private SphereCollider _aoeCollizion;
    private List<GameObject> _targetsList;

    private TowerEnum _towerEnum;
    private Vector3 _movement;

    private bool _isStatic;
    private float _tolerance = 0.5f;

    private float _duration;
    private float _timeDOT;
    private float _damage;
    private int _maxDistance;

    private bool _firstUpgrade;
    private bool _secondUpgrade;
    private bool _thirdUpgrade;
    private float _updateFloatTimerValue;
    private float _updateFloatDamageValue;
    private float _updateFloatRadiusValue;
    private int _updateIntDamageValue;
    private int _updateIntDistanceValue;
    private int _updateIntAmountValue;

    public GameObject BulletsCurrentTarget { get => _bulletsCurrentTarget; set => _bulletsCurrentTarget = value; }
    public TowerScriptable TowerSO { get => _towerSO; set => _towerSO = value; }
    public Transform StartBulletPosition { get => _startBulletPosition; set => _startBulletPosition = value; }


    public bool FirstUpgrade { get => _firstUpgrade; set => _firstUpgrade = value; }
    public bool SecondUpgrade { get => _secondUpgrade; set => _secondUpgrade = value; }
    public bool ThirdUpgrade { get => _thirdUpgrade; set => _thirdUpgrade = value; }
    public int UpdateIntValue { get => _updateIntDamageValue; set => _updateIntDamageValue = value; }
    public float UpdateFloatTimerValue { get => _updateFloatTimerValue; set => _updateFloatTimerValue = value; }
    public float UpdateFloatDamageValue { get => _updateFloatDamageValue; set => _updateFloatDamageValue = value; }
    public float UpdateFloatRadiusValue { get => _updateFloatRadiusValue; set => _updateFloatRadiusValue = value; }
    public int UpdateIntDistanceValue { get => _updateIntDistanceValue; set => _updateIntDistanceValue = value; }
    public List<GameObject> TargetsList { get => _targetsList; set => _targetsList = value; }
    public int UpdateIntAmountValue { get => _updateIntAmountValue; set => _updateIntAmountValue = value; }

    private void Start()
    {
        _currentEnemyHealth = _bulletsCurrentTarget.GetComponent<EnemyParametrs>();
        _bulletRB = GetComponent<Rigidbody>();

        _towerEnum = _towerSO.TowerEnum;
        _maxDistance = _towerSO.MaxBulletDistance;
        _timeDOT = _towerSO.BulletDOTTime;
        _duration = _towerSO.BulletDuration;
        _damage = _towerSO.TowerDamage;

        if (_towerSO.TowerEnum == TowerEnum.Catapult)
        {
            _aoeCollizion = gameObject.GetComponentInChildren<SphereCollider>();

            if (_firstUpgrade)
            {
                IncreaseAoeRange();
            }
        }

        if (_towerSO.TowerEnum == TowerEnum.Cannon && _secondUpgrade)
        {
            UpdateDamage();
        }

        if (_towerSO.TowerEnum == TowerEnum.Catapult && _thirdUpgrade)
        {
            UpdateTimerTime();
        }

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
        if (_currentEnemyHealth.HasDamageWeekness)
        {
            _damage = _damage * _updateFloatDamageValue;
        }

        if (_towerEnum == TowerEnum.Cannon)
        {
            SetDirectionToTarget(_bulletsCurrentTarget);
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
                DealDamage(enemy, _damage);
            }

            if (_towerEnum != TowerEnum.Sniper && _towerEnum != TowerEnum.Catapult)
            {
                bool isAlreadyRecoil = false;

                if (_towerEnum == TowerEnum.Cannon && _thirdUpgrade && !isAlreadyRecoil)
                {
                    SetNewTarget();
                    isAlreadyRecoil = true;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            if (_towerEnum == TowerEnum.Sniper && _secondUpgrade)
            {
                DealDOTDamage();
            }
        }
    }

    private void DealDamage(EnemyParametrs enemy, float damage)
    {
        float colorValue = enemy.CurrentPaintValue;
        float currentHealth = _currentEnemyHealth.CurrentHealth;

        switch (colorValue)
        {
            case 0: currentHealth -= 0; break;
            case 1: currentHealth = currentHealth - (damage * _firstPaintingStage); break;
            case 2: currentHealth = currentHealth - (damage * _secondPaintingStage); break;
            case 3: currentHealth = currentHealth - (damage * _thirdPaintingStage); break;
            case 4: currentHealth = currentHealth - (damage * _fourthPaintingStage); break;
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

    private void SetDirectionToTarget(GameObject target)
    {
        Vector3 move = (target.transform.position - gameObject.transform.position).normalized;
        _movement.Set(_speed * move.x, _speed * move.y, 0);
    }

    private void SetDirectionToTargetOnce()
    {
        SetDirectionToTarget(_bulletsCurrentTarget);
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
        _aoeBullet.GetComponent<TowerAOEBulletTriggerZone>().ParentTowerEnum = _towerEnum;
        _aoeBullet.GetComponent<TowerAOEBulletTriggerZone>().Upgrade = _secondUpgrade;
    }

    private void DamageOverTime()
    {
        List<EnemyParametrs> targetList = _bulletTriggerZone.TargetInAOE;

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
        if (_towerSO.TowerEnum == TowerEnum.Sniper && _thirdUpgrade)
        {
            IncreasedBulletLifeDistance();
        }

        float distance = Vector3.Distance(_startBulletPosition.position, gameObject.transform.position);
        if (distance >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    #region addLiseners
    private void UpdateDamage()
    {
        _damage += _updateIntDamageValue;
    }

    private void IncreaseAoeRange()
    {
        _aoeCollizion.radius += _updateFloatRadiusValue;
    }

    private void UpdateTimerTime()
    {
        _timeDOT -= _updateFloatTimerValue;
    }

    private void IncreasedBulletLifeDistance()
    {
        _maxDistance += _updateIntDistanceValue;
    }

    private void SetNewTarget()
    {
        System.Random rnd = new System.Random();
        int randIndex = rnd.Next(_targetsList.Count);

        SetDirectionToTarget(_targetsList[randIndex]);
    }

    private void DealDOTDamage()
    {
        _timerUpgradeDOT = new Timer(_updateFloatTimerValue);

        int repeatAmount = 0;
        ReloadDOTTimer(repeatAmount);
    }

    private void ReloadDOTTimer(int repeatAmount)
    {
        if (repeatAmount < _updateIntAmountValue)
        {
            StartDOTTimerReload(repeatAmount);
        }
    }

    private void StartDOTTimerReload(int repeatAmount)
    {
         _timerUpgradeDOT.Wait();

        if (_bulletsCurrentTarget != null)
        {
            if (!_timerUpgradeDOT.StartTimer)
            {
                _timerUpgradeDOT.StartCountdown();
            }

            if (_timerUpgradeDOT.ReachingTimerMaxValue == true)
            {
                _timerUpgradeDOT.StopCountdown();
                repeatAmount += 1;

                DealDamage(_currentEnemyHealth, _updateFloatDamageValue);
                ReloadDOTTimer(repeatAmount);
            }
        }
    }
    #endregion
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
}