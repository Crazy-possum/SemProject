using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerAttak : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private float _rotationSpeed = 2;

    private Timer _attakTimer;

    //public EconomyController EconomyController;
    public GameObject CurrentTarget;

    private void Start()
    {
        _attakTimer = new Timer(1);
    }

    private void Update()
    {
        _attakTimer.Wait();

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
                _attakTimer.StopCountdown();
            }
        }

        //Сломаный поворот башни на противника
       // Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;
       // Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        //transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    public void Attak()
    {
        Vector3 position = _bulletSpawner.position;
        GameObject localBullet = GameObject.Instantiate(_bullet, position, Quaternion.identity, _bulletSpawner);

        localBullet.GetComponent<BulletBehavior>().BulletsCurrentTarget = CurrentTarget;
    }

    private void TowerRotate()
    {

           // gameObject.transform.LookAt(CurrentTarget.transform);
    }

}

