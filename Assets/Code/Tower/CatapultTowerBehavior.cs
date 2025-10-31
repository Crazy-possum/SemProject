using System.Collections.Generic;
using UnityEngine;

public partial class CatapultTowerBehavior : TowerBehavior
{
    public CatapultTowerBehavior(TowerScriptable towerSO, Rigidbody rb, Timer reloadTimer, GameObject bulletPref, GameObject towerObject, Transform bulletSpawner) :
        base(towerSO, rb, reloadTimer, bulletPref, towerObject, bulletSpawner)
    {
        _towerSO = towerSO;
        _towerRb = rb;
        _attakTimer = reloadTimer;
        _towerBulletPrefab = bulletPref;
        _towerObject = towerObject;
        _bulletSpawner = bulletSpawner;
        _targetsList = new List<GameObject>();
    }

    public override void SetTarget()
    {
        if (_currentTarget != null)
        {
            return;
        }
        if (TargetsList != null && TargetsList.Count > 0)
        {
            System.Random rnd = new System.Random();
            int index = rnd.Next(TargetsList.Count);
            _currentTarget = TargetsList[index];
        }
    }
}

