using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior
{
    protected TowerScriptable _towerSO;
    protected List<GameObject> _targetsList;
    protected Timer _attakTimer;

    protected GameObject _towerBulletPrefab;
    protected GameObject _currentTarget;
    protected GameObject _towerObject;
    protected Transform _bulletSpawner;
    protected Rigidbody _towerRb;

    protected TowerTargetEnum _towerAtkPattern;

    protected float _attakReload;
    private float _towerRange;

    private bool _firstUpgrade = false;
    private bool _secondUpgrade = false;
    private bool _thirdUpgrade = false;
    private int _updateIntValue = 0;
    private float _updateFloatValue = 0;

    public List<GameObject> TargetsList { get => _targetsList; set => _targetsList = value; }

    public TowerBehavior(TowerScriptable towerSO, Rigidbody rb, Timer reloadTimer, 
        GameObject bulletPref, GameObject towerObject, Transform bulletSpawner)
    {
        _towerSO = towerSO;
        _towerRb = rb;
        _attakTimer = reloadTimer;
        _towerBulletPrefab = bulletPref;
        _towerObject = towerObject;
        _bulletSpawner = bulletSpawner;
        _towerRange = _towerObject.GetComponentInChildren<TowerTriggerZone>().gameObject.GetComponent<SphereCollider>().radius;
        _targetsList = new List<GameObject>();

        if (_secondUpgrade && _towerSO.TowerEnum == TowerEnum.Shotgun)
        {
            _towerRange = _towerSO.TowerRange + _updateFloatValue; //------------Liseners------------
        }
        else
        {
            _towerRange = _towerSO.TowerRange;
        }

        AddLiseners();
    }

    public void AddLiseners()
    {
        TowerUpgrader.OnActivateCannonSecondUpgrade += ActivateCannonSecondUpdate;
        TowerUpgrader.OnActivateShotgunFirstUpgrade += ActivateShotgunFirstUpdate;
        TowerUpgrader.OnActivateShotgunSecondUpgrade += ActivateShotgunSecondUpdate;
        TowerUpgrader.OnActivateShotgunThirdUpgrade += ActivateShotgunThirdUpdate;
        TowerUpgrader.OnActivateCatapultFirstUpgrade += ActivateCatapultFirstUpgrade;
    }

    public virtual void SetTarget()
    {
        if (_currentTarget != null)
        {
            return;
        }
        if (TargetsList != null && TargetsList.Count > 0)
        {
            _currentTarget = TargetsList[0];
        }
    }

    public void SpawnBullet()
    {
        if (_firstUpgrade && _towerSO.TowerEnum == TowerEnum.Shotgun)
        {
            int bulletAmount = _towerSO.BulletAmount + _updateIntValue; //------------Liseners------------
        }
        else
        {
            int bulletAmount = _towerSO.BulletAmount;
        }

        for (int i = 0; i < _towerSO.BulletAmount; i++)
        {
            Vector3 position = _bulletSpawner.position;
            GameObject localBullet = GameObject.Instantiate(_towerBulletPrefab, position, Quaternion.identity, _bulletSpawner);

            TowerBulletBehavior towerBulletBehavior = localBullet.GetComponent<TowerBulletBehavior>();

            towerBulletBehavior.BulletsCurrentTarget = _currentTarget;
            towerBulletBehavior.StartBulletPosition = _bulletSpawner;
            towerBulletBehavior.TowerSO = _towerSO;

            towerBulletBehavior.FirstUpgrade = _firstUpgrade;
            towerBulletBehavior.SecondUpgrade = _secondUpgrade;
            towerBulletBehavior.ThirdUpgrade = _thirdUpgrade;
            towerBulletBehavior.UpdateFloatValue = _updateFloatValue;
            towerBulletBehavior.UpdateIntValue = _updateIntValue;
            //localBullet.GetComponent<TowerBulletBehavior>()
        }
    }


    public void TowerRotate()
    {
        Vector3 targetDirection = _towerObject.transform.position - _currentTarget.transform.position;

        Quaternion angle = Quaternion.LookRotation(Vector3.forward, targetDirection);
        _towerRb.rotation = angle;
    }

    public virtual void RealoadTimer()
    {
        _attakTimer.Wait();

        if (_currentTarget != null)
        {
            if (!_attakTimer.StartTimer)
            {
                _attakTimer.StartCountdown();
            }

            if (_attakTimer.ReachingTimerMaxValue == true)
            {
                TowerRotate();
                AttakTarget();
                _attakTimer.StopCountdown();
            }
        }
    }

    public virtual void AttakTarget()
    {
        SpawnBullet(); 
    }

    //-----------------------Liseners--------------------------------------------------------------------------

    public void ActivateCannonSecondUpdate(int addDamage, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _secondUpgrade = true;
            _updateIntValue = addDamage;
        }
    }

    public void ActivateShotgunFirstUpdate(int addBulletAmount, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _firstUpgrade = true;
            _updateIntValue = addBulletAmount;
        }
    }

    private void ActivateShotgunSecondUpdate(float addTowerRange, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _secondUpgrade = true;
            _updateFloatValue = addTowerRange;
        }
    }

    private void ActivateShotgunThirdUpdate(float cutReload, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _thirdUpgrade = true;
            _updateFloatValue = cutReload;

            tower.GetComponent<TowerAttak>().AttakReload -= cutReload;
        }
    }

    private void ActivateCatapultFirstUpgrade(float addAoeRange, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _firstUpgrade = true;
            _updateFloatValue = addAoeRange;
        }
    }
}

