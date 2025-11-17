using System;
using System.Collections.Generic;
using UnityEngine;

public partial class CatapultTowerBehavior
{
    public class SniperTowerBehavior : TowerBehavior
    {
        private List<EnemyParametrs> _enemyParametrsList;
        private float _lastMorePaint = 1;
        private float _currentMorePaint;
        private bool _isPaintChanged = true;

        public SniperTowerBehavior(TowerScriptable towerSO, Rigidbody rb, Timer reloadTimer, GameObject bulletPref, GameObject towerObject, Transform bulletSpawner, GameObject bulletSpawnerGO) :
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
                _enemyParametrsList = CreateEnemyParametrsList();

                List<EnemyParametrs> targetPaintList = CreateTargetPaintList();

                float morePaint = DetermineMorePaintValue(targetPaintList);

                if (morePaint != _lastMorePaint)
                {
                    _lastMorePaint = morePaint;
                    _isPaintChanged = true;
                }
                else
                {
                    _isPaintChanged = false;
                }

                if (_isPaintChanged)
                {
                    _currentTarget = DetermineTarget(morePaint);
                }
            }
        }

        private List<EnemyParametrs> CreateEnemyParametrsList()
        {
            List<EnemyParametrs> enemyParametrsList = new List<EnemyParametrs>();

            foreach (GameObject target in TargetsList)
            {
                EnemyParametrs parametrs = target.GetComponent<EnemyParametrs>();
                enemyParametrsList.Add(parametrs);
            }

            return enemyParametrsList;
        }

        private List<EnemyParametrs> CreateTargetPaintList()
        {
            List<EnemyParametrs> targetPaintList = new List<EnemyParametrs>();

            float moreHp = _enemyParametrsList[0].CurrentHealth;

            for (int i = 0; i < _enemyParametrsList.Count; i++)
            {
                if (moreHp != _enemyParametrsList[i].CurrentHealth)
                {
                    moreHp = Math.Max(moreHp, _enemyParametrsList[i].CurrentHealth);
                }
            }

            foreach (EnemyParametrs enemyParametrs in _enemyParametrsList)
            {
                if (enemyParametrs.CurrentHealth == moreHp)
                {
                    targetPaintList.Add(enemyParametrs);
                }
            }

            return targetPaintList;
        }

        private float DetermineMorePaintValue(List<EnemyParametrs> targetPaintList)
        {
            float morePaint = targetPaintList[0].CurrentPaintValue;

            for (int i = 0; i < targetPaintList.Count; i++)
            {
                float tPL = targetPaintList[i].CurrentPaintValue;

                if (tPL != morePaint)
                {
                    morePaint = Math.Max(morePaint, tPL);
                }
            }

            return morePaint;
        }

        private GameObject DetermineTarget(float morePaint)
        {
            List<GameObject> forRandomList = new List<GameObject>();

            for (int i = 0; i < TargetsList.Count; i++)
            {
                if (_enemyParametrsList[i].CurrentPaintValue == morePaint)
                {
                    forRandomList.Add(TargetsList[i]);
                }
            }

            System.Random rnd = new System.Random();
            int randIndex = rnd.Next(forRandomList.Count);
            return forRandomList[randIndex];
        }
    }
}

