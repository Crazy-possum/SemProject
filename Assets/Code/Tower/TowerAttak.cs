using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static CatapultTowerBehavior;

public class TowerAttak : MonoBehaviour
{
    [Tooltip("Префаб башенной пули")]
    [SerializeField] private GameObject _towerBulletPrefab;
    [Tooltip("Точка, из которой вылетают пули")]
    [SerializeField] private Transform _bulletSpawner;
    [Tooltip("Таймер перезарядки в сек")]
    [SerializeField] private float _attakReload = 1;

    private TowerBehavior _towerBehavior;
    private List<GameObject> _targetsList;
    private TowerScriptable _towerSO;
    private GameObject _currentTarget;
    private Rigidbody _towerRb;
    private Timer _attakTimer;

    private TowerTargetEnum _towerAtkPattern;
    private TowerEnum _towerEnum;

    public List<GameObject> TargetsList { get => _targetsList; set => _targetsList = value; }
    public TowerScriptable TowerSO { get => _towerSO; set => _towerSO = value; }
    public GameObject CurrentTarget { get => _currentTarget; set => _currentTarget = value; }

    private void Start()
    {
        _attakTimer = new Timer(_attakReload);
        _towerRb = GetComponent<Rigidbody>();
        _towerAtkPattern = _towerSO.TowerAtkPattern;
        _towerEnum = _towerSO.TowerEnum;

        if (_towerEnum == TowerEnum.Cannon)
        {
            _towerBehavior = new TowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner);
            //TowerAtkPatternEnum
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
            //PatternDetermine();
            _towerBehavior.TowerRotate();
        }
    }

    public void SetTargetList(List<GameObject> targetsList)
    {
        _towerBehavior.TargetsList = targetsList;
    }
}

