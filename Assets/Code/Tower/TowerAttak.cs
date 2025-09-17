using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttak : MonoBehaviour
{
    [SerializeField] private Timer _attakTimer;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawner;

    public GameObject CurrentTarget;

    private EnemyHealth _currentEnemyHealth;

    private void Start()
    {
        _attakTimer.MaxTimerValue = 2;
    }

    private void Update()
    {
        if (CurrentTarget != null)
        {
            Debug.Log(CurrentTarget);
            Debug.Log(_attakTimer.MaxTimerValue);
            _currentEnemyHealth = CurrentTarget.GetComponent<EnemyHealth>();

            if (!_attakTimer.StartTimer)
            {
                _attakTimer.StartCountdown();

                if (_attakTimer.ReachingTimerMaxValue == true)
                {
                    Attak();
                    _attakTimer.StopCountdown();
                }
            }
        }
    }

    public void Attak()
    {
        _currentEnemyHealth.CurrentHealth = _currentEnemyHealth.CurrentHealth - 2;
        Debug.Log(_currentEnemyHealth);

        //Vector3 position = _bulletSpawner.position;
        //GameObject localBullet = GameObject.Instantiate(_bullet, position, Quaternion.identity, _bulletSpawner);

        //localBullet.GetComponent<BulletBehavior>().BulletsCurrentTarget = CurrentTarget;
    }

}

