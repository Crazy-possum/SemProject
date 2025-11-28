using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior
{
    protected TowerSO _towerSO;
    protected List<GameObject> _targetsList;
    protected Timer _attakTimer;

    protected GameObject _towerBulletPrefab;
    protected GameObject _currentTarget;
    protected GameObject _towerObject;
    protected Rigidbody _bulletSpawnerRb;
    protected Transform _bulletSpawner;
    protected Rigidbody _towerRb;
    protected SphereCollider _towerTriggerCollizion;

    protected TowerTargetEnum _towerAtkPattern;

    protected float _attakReload;
    protected float _currentReloadTime;

    protected bool _firstUpgrade = false;
    protected bool _secondUpgrade = false;
    protected bool _thirdUpgrade = false;
    protected int _updateIntDamageValue = 0;
    protected int _updateIntAmountValue = 0;
    protected int _updateIntDistanceValue = 0;
    protected float _updateFloatTimerValue = 0;
    protected float _updateFloatRadiusValue = 0;
    protected float _updateFloatDamageValue = 0;

    protected float _charFloatDamageUpgrade = 1;
    protected float _charFloatValueUpgrade = 1;
    protected float _charRadiusUpgrade = 1;
    protected float _upgradeFloatDamageWeeknessBonus = 1;

    private Timer _doubleShotTimer;

    public List<GameObject> TargetsList { get => _targetsList; set => _targetsList = value; }

    public TowerBehavior(TowerSO towerSO, Rigidbody rb, Timer reloadTimer,
        GameObject bulletPref, GameObject towerObject, Transform bulletSpawner, GameObject bulletSpawnerGO)
    {
        _towerSO = towerSO;
        _towerRb = rb;
        _attakTimer = reloadTimer;
        towerObject.GetComponent<TowerAttak>().AttakReload = towerSO.TowerReloadTime;
        _towerBulletPrefab = bulletPref;
        _towerObject = towerObject;
        _bulletSpawner = bulletSpawner;
        _bulletSpawnerRb = bulletSpawnerGO.GetComponent<Rigidbody>();
        _towerTriggerCollizion = _towerObject.GetComponentInChildren<TowerTriggerZone>().gameObject.GetComponent<SphereCollider>();
        _targetsList = new List<GameObject>();
        _currentReloadTime = _attakReload;

        if (_secondUpgrade && _towerSO.TowerEnum == TowerEnum.Shotgun)
        {
            _towerTriggerCollizion.radius = (_towerSO.TowerRange + _updateFloatRadiusValue) * _charRadiusUpgrade; //------------Liseners------------
        }
        else
        {
            _towerTriggerCollizion.radius = _towerSO.TowerRange * _charRadiusUpgrade;
        }

        AddLiseners();
    }

    public void AddLiseners()
    {
        TowerUpgrader.OnActivateCannonFirstUpgrade += ActivateCannonFirstUpgrade; 
        TowerUpgrader.OnActivateCannonSecondUpgrade += ActivateCannonSecondUpdate;
        TowerUpgrader.OnActivateCannonThirdUpgrade += ActivateCannonThierdUpgrade;
        TowerUpgrader.OnActivateShotgunFirstUpgrade += ActivateShotgunFirstUpdate;
        TowerUpgrader.OnActivateShotgunSecondUpgrade += ActivateShotgunSecondUpdate;
        TowerUpgrader.OnActivateShotgunThirdUpgrade += ActivateShotgunThirdUpdate;
        TowerUpgrader.OnActivateCatapultFirstUpgrade += ActivateCatapultFirstUpgrade;
        TowerUpgrader.OnActivateCatapultSecondUpgrade += ActivateCatapultSecondUpgrade;
        TowerUpgrader.OnActivateCatapultThirdUpgrade += ActivateCatapultThirdUpgrade;
        TowerUpgrader.OnActivateSniperFirstUpgrade += ActivateSniperFirstUpgrade;
        TowerUpgrader.OnActivateSniperSecondUpgrade += ActivateSniperSecondUpgrade;
        TowerUpgrader.OnActivateSniperThirdUpgrade += ActivateSniperThirdUpgrade;

        CharacterUpgrader.OnIncreaseTowerDamage += ActivateCharUpgradeTowerDamage;
        CharacterUpgrader.OnIncreaseTowerRadius += ActivateCharUpgradeTowerRadius;
        CharacterUpgrader.OnSpeedUpTowerReload += ActivateCharUpgradeTowerReload;
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
        int bulletAmount = _towerSO.BulletAmount;

        if (_firstUpgrade && _towerSO.TowerEnum == TowerEnum.Shotgun)
        {
            bulletAmount = _towerSO.BulletAmount + _updateIntAmountValue; //------------Liseners------------
        }

        for (int i = 0; i < bulletAmount; i++)
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
            towerBulletBehavior.UpgateFloatTimerValue = _updateFloatTimerValue;
            towerBulletBehavior.UpgateFloatRadiusValue = _updateFloatRadiusValue;
            towerBulletBehavior.UpgateFloatDamageValue = _updateFloatDamageValue;
            towerBulletBehavior.UpgradeFloatDamageWeeknessBonus = _upgradeFloatDamageWeeknessBonus;
            towerBulletBehavior.UpgateIntValue = _updateIntDamageValue;
            towerBulletBehavior.UpgateIntDistanceValue = _updateIntDistanceValue;
            towerBulletBehavior.UpgateIntAmountValue = _updateIntAmountValue;
            towerBulletBehavior.CharacterFloatUpgrade = _charFloatDamageUpgrade;
            towerBulletBehavior.TargetsList = _targetsList;
        }
    }

    public void TowerRotate()
    {
        Vector3 targetDirection = _towerObject.transform.position - _currentTarget.transform.position;

        Quaternion angle = Quaternion.LookRotation(Vector3.forward, targetDirection);
        _bulletSpawnerRb.rotation = angle;
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
                if (_towerSO.TowerEnum == TowerEnum.Cannon && _firstUpgrade)
                {
                    ReloadDoubleShotTimer();
                }

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
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    #region addLiseners
    public void ActivateCannonFirstUpgrade(float newTimerTime, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _firstUpgrade = true;
            AddDoubleShot(newTimerTime);
        }
    }

    public void ActivateCannonSecondUpdate(int addDamage, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _secondUpgrade = true;
            _updateIntDamageValue = addDamage;
        }
    }

    public void ActivateCannonThierdUpgrade(GameObject tower)
    {
        if (_towerObject == tower)
        {
            _thirdUpgrade = true;
        }
    }

    public void ActivateShotgunFirstUpdate(int addBulletAmount, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _firstUpgrade = true;
            _updateIntAmountValue = addBulletAmount;
        }
    }

    public void ActivateShotgunSecondUpdate(float addTowerRange, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _secondUpgrade = true;
            _towerObject.GetComponentInChildren<TowerTriggerZone>().GetComponent<SphereCollider>().radius *= addTowerRange; 
        }
    }

    public void ActivateShotgunThirdUpdate(float cutReload, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _thirdUpgrade = true;
        }
    }

    public void ActivateCatapultFirstUpgrade(float addAoeRange, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _firstUpgrade = true;
            _updateFloatRadiusValue = addAoeRange;
        }
    }

    public void ActivateCatapultSecondUpgrade(float damageBonus, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _secondUpgrade = true;
        }
        _upgradeFloatDamageWeeknessBonus = damageBonus;
    }

    public void ActivateCatapultThirdUpgrade(float cutDotTriggeredTime, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _thirdUpgrade = true;
            _updateFloatTimerValue = cutDotTriggeredTime;
        }
    }

    public void ActivateSniperFirstUpgrade(float cutReload, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _firstUpgrade = true;
        }
    }

    public void ActivateSniperSecondUpgrade(float dotDamage, float timerTime, int count, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _secondUpgrade = true;
            _updateFloatDamageValue = dotDamage;  
            _updateFloatTimerValue = timerTime;
            _updateIntAmountValue = count;
        }
    }

    public void ActivateSniperThirdUpgrade(int addDistance, GameObject tower)
    {
        if (_towerObject == tower)
        {
            _thirdUpgrade = true;
            _updateIntDistanceValue = addDistance;
        }
    }

    #region CharacterUpgrade
    private void ActivateCharUpgradeTowerDamage(float towerDamage)
    {
        _charFloatDamageUpgrade = towerDamage;
    }

    private void ActivateCharUpgradeTowerRadius(float towerRange)
    {
        _charRadiusUpgrade = towerRange;

        if (_secondUpgrade && _towerSO.TowerEnum == TowerEnum.Shotgun)
        {
            _towerTriggerCollizion.radius = (_towerSO.TowerRange + _updateFloatRadiusValue) * _charRadiusUpgrade; //------------Liseners------------
        }
        else
        {
            _towerTriggerCollizion.radius = _towerSO.TowerRange * _charRadiusUpgrade;
        }
    }

    private void ActivateCharUpgradeTowerReload(float cutCharReload)
    {
        _currentReloadTime = _currentReloadTime - (_attakReload * cutCharReload);
        _attakTimer.ResetTimerMaxTime(_currentReloadTime);
    }
    #endregion

    #endregion
    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    private void AddDoubleShot(float newTimerTime)
    {
        _doubleShotTimer = new Timer(newTimerTime);
    }

    private void ReloadDoubleShotTimer()
    {
        _doubleShotTimer.Wait();
        if (_currentTarget != null)
        {
            if (!_doubleShotTimer.StartTimer)
            {
                _doubleShotTimer.StartCountdown();
            }

            if (_doubleShotTimer.ReachingTimerMaxValue == true)
            {
                TowerRotate();
                AttakTarget();
                _doubleShotTimer.StopCountdown();
            }
            else
            {
                ReloadDoubleShotTimer();
            }
        }
    }
}

