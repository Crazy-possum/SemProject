using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawner;
    //[SerializeField] private float _rotationSpeed = 2;

    public float AttakReload = 2;
    public float CurrentTime;

    private Timer _attakReloadTimer;
    private bool _canShoot = false;

    private void Start()
    {
        _attakReloadTimer = new Timer(AttakReload);
    }

    private void Update()
    {
        Reload();

        if (_canShoot)
        {
            Fire();
        }

        CurrentTime = _attakReloadTimer.TimerCurrentTime;
    }

    private void Reload()
    {
         _attakReloadTimer.Wait();

        if (!_attakReloadTimer.StartTimer && !_canShoot)
        {
            _attakReloadTimer.StartCountdown();
        }

        if (_attakReloadTimer.ReachingTimerMaxValue == true)
        {
            _canShoot = true;
            _attakReloadTimer.PauseCountdown();
        }
    }

    private void Fire()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0)))
        {
            Vector3 position = _bulletSpawner.position;
            GameObject localBullet = GameObject.Instantiate(_bullet, position, Quaternion.identity, _bulletSpawner);
            localBullet.GetComponent<CharacterBulletBehavior>().StartBulletPosition = _bulletSpawner;

            _attakReloadTimer.StopCountdown();
            _canShoot = false;
        }
    }
}
