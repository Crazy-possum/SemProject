using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttak : MonoBehaviour
{
    [SerializeField] private Timer _attakTimer;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawner;

    public GameObject CurrentTarget;

    private void Start()
    {
        _attakTimer.MaxTimerValue = 1;
    }

    private void Update()
    {
        if (CurrentTarget != null)
        {
            if (!_attakTimer.StartTimer)
            {
                _attakTimer.StartCountdown();
            }

            if (_attakTimer.ReachingTimerMaxValue == true)
            {
                Attak();
                TowerRotate();
                _attakTimer.TimerCurrentTime = 0;
                _attakTimer.StopCountdown();
            }
        }
    }

    public void Attak()
    {
        Vector3 position = _bulletSpawner.position;
        GameObject localBullet = GameObject.Instantiate(_bullet, position, Quaternion.identity, _bulletSpawner);

        localBullet.GetComponent<BulletBehavior>().BulletsCurrentTarget = CurrentTarget;
    }

    private void TowerRotate()
    {
       // Ray ray = new Ray(transform.position, transform.forward);

            gameObject.transform.LookAt(CurrentTarget.transform);
    }

}

