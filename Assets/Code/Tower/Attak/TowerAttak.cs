using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental;
using static CatapultTowerBehavior;

public class TowerAttak : MonoBehaviour
{
    [Tooltip("Точка, из которой вылетают пули")]
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private GameObject _bulletSpawnerGO;
    [Tooltip("Таймер перезарядки в сек")]
    private float _attakReload;

    private TowerBehavior _towerBehavior;
    private List<GameObject> _targetsList;
    private TowerSO _towerSO;
    private PurchasedUpgrade _purchasedUpgrade;
    private Timer _attakTimer;

    private GameObject _towerBulletPrefab;
    private GameObject _currentTarget;
    private Rigidbody _towerRb;

    private TowerEnum _towerEnum;

    private bool _firstUpgrade;
    private bool _secondUpgrade;
    private bool _thirdUpgrade;


    public List<GameObject> TargetsList { get => _targetsList; set => _targetsList = value; }
    public TowerSO TowerSO { get => _towerSO; set => _towerSO = value; }
    public GameObject CurrentTarget { get => _currentTarget; set => _currentTarget = value; }
    public float AttakReload { get => _attakReload; set => _attakReload = value; }

    private void Start()
    {
        _towerRb = GetComponent<Rigidbody>();
        _towerEnum = _towerSO.TowerEnum;
        _towerBulletPrefab = _towerSO.BulletPrefab;

        SetReloatTimer();

        if (_towerEnum == TowerEnum.Cannon)
        {
            _towerBehavior = new TowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }
        else if (_towerEnum == TowerEnum.Shotgun)
        {
            _towerBehavior = new ShotgunTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }
        else if (_towerEnum == TowerEnum.Catapult)
        {
            _towerBehavior = new CatapultTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }
        else if (_towerEnum == TowerEnum.Sniper)
        {
            _towerBehavior = new SniperTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }

        if (_towerBehavior == null)
        {
            _towerBehavior = new TowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }
    }

    private void FixedUpdate()
    {
        _towerBehavior.SetTarget();
        _towerBehavior.RealoadTimer();

        if (_towerBehavior.TargetsList.Count > 0)
        {
            _towerBehavior.TowerRotate();
        }
    }

    private void OnEnable()
    {
        TowerUpgrader.OnActivateShotgunThirdUpgrade += ResetShotgunReloadTimerTime;
        TowerUpgrader.OnActivateSniperFirstUpgrade += ResetSniperReloadTimerTime;
    }

    private void OnDisable()
    {
        TowerUpgrader.OnActivateShotgunThirdUpgrade -= ResetShotgunReloadTimerTime;
        TowerUpgrader.OnActivateSniperFirstUpgrade -= ResetSniperReloadTimerTime;
    }

    public void SetTargetList(List<GameObject> targetsList)
    {
        _towerBehavior.TargetsList = targetsList;
    }

    //-----------------------Liseners--------------------------------------------------------------------------

    public void SetReloatTimer()
    {
        AttakReload = _towerSO.TowerReloadTime;
        _attakTimer = new Timer(AttakReload);
    }

    public void ResetShotgunReloadTimerTime(float cutReload, GameObject tower)
    {
        if(_towerEnum == TowerEnum.Shotgun)
        {
            _attakTimer.ResetTimerMaxTime(_attakReload * cutReload);
            _attakReload *= cutReload;
        }
    }

    public void ResetSniperReloadTimerTime(float cutReload, GameObject tower)
    {
        if (_towerEnum == TowerEnum.Sniper)
        {
            _attakTimer.ResetTimerMaxTime(_attakReload * cutReload);
            _attakReload *= cutReload;
        }
    }
}

