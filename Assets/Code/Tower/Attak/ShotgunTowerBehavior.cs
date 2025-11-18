using System;
using System.Collections.Generic;
using UnityEngine;

public partial class CatapultTowerBehavior
{
    public class ShotgunTowerBehavior : TowerBehavior
    {
        public ShotgunTowerBehavior(TowerScriptable towerSO, Rigidbody rb, Timer reloadTimer, GameObject bulletPref, GameObject towerObject, Transform bulletSpawner, GameObject bulletSpawnerGO) :
        base(towerSO, rb, reloadTimer, bulletPref, towerObject, bulletSpawner, bulletSpawnerGO)
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
            if (TargetsList != null && TargetsList.Count > 0)
            {
                List<float> distanceToTargetList = new List<float>();

                foreach (GameObject target in TargetsList)
                {
                    float distance = Vector3.Distance(_towerObject.transform.position, target.transform.position);
                    distanceToTargetList.Add(distance);
                }

                float minimum = distanceToTargetList[0];

                for (int i = 0; i < distanceToTargetList.Count; i++)
                {
                    minimum = Math.Min(minimum, distanceToTargetList[i]);
                }

                int index = distanceToTargetList.IndexOf(minimum);
                _currentTarget = TargetsList[index];
            }
        }
    }
}

