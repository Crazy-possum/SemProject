using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior
{
    protected TowerScriptable _towerSO;
    protected List<GameObject> _targetsList;
    protected GameObject _towerBulletPrefab;
    protected GameObject _currentTarget;
    protected GameObject _towerObject;
    protected Transform _bulletSpawner;
    protected Rigidbody _towerRb;
    protected Timer _attakTimer;

    protected TowerTargetEnum _towerAtkPattern;

    protected float _attakReload;

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
        _targetsList = new List<GameObject>();
    }

    public void SpawnBullet()
    {
        Vector3 position = _bulletSpawner.position;
        GameObject localBullet = GameObject.Instantiate(_towerBulletPrefab, position, Quaternion.identity, _bulletSpawner);

        localBullet.GetComponent<TowerBulletBehavior>().BulletsCurrentTarget = _currentTarget;
        localBullet.GetComponent<TowerBulletBehavior>().TowerSO = _towerSO;
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

    public virtual void SetTarget()
    {
        if(_currentTarget != null)
        {
            return;
        }
        if (TargetsList != null && TargetsList.Count > 0)
        {
            _currentTarget = TargetsList[0];
        }
    }

    public virtual void AttakTarget()
    {
        SpawnBullet();
    }
}

