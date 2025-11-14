using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static CatapultTowerBehavior;

public class TowerAttak : MonoBehaviour
{
    [Tooltip("Точка, из которой вылетают пули")]
    [SerializeField] private Transform _bulletSpawner;
    [Tooltip("Таймер перезарядки в сек")]

    public float AttakReload;

    private TowerBehavior _towerBehavior;
    private List<GameObject> _targetsList;
    private TowerScriptable _towerSO;
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
    public TowerScriptable TowerSO { get => _towerSO; set => _towerSO = value; }
    public GameObject CurrentTarget { get => _currentTarget; set => _currentTarget = value; }

    private void Start()
    {
        _towerRb = GetComponent<Rigidbody>();
        _towerEnum = _towerSO.TowerEnum;
        _towerBulletPrefab = _towerSO.BulletPrefab;

        SetReloatTimer();

        if (_towerEnum == TowerEnum.Cannon)
        {
            _towerBehavior = new TowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner);
        }
        else if (_towerEnum == TowerEnum.Shotgun)
        {
            _towerBehavior = new ShotgunTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner);
        }
        else if (_towerEnum == TowerEnum.Catapult)
        {
            _towerBehavior = new CatapultTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner);
        }
        else if (_towerEnum == TowerEnum.Sniper)
        {
            _towerBehavior = new SniperTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner);
        }

        if (_towerBehavior == null)
        {
            _towerBehavior = new TowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner);
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

    public void SetTargetList(List<GameObject> targetsList)
    {
        _towerBehavior.TargetsList = targetsList;
    }

    public void SetReloatTimer()
    {
        AttakReload = _towerSO.TowerReloadTime;
        _attakTimer = new Timer(AttakReload);
    }
}

