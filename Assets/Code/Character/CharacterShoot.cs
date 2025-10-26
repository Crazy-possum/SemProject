using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CharacterShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _body;
    [SerializeField] private Transform _bulletSpawner;

    public float AttakReload = 2;
    public float CurrentTime;

    private Timer _attakReloadTimer;
    private Transform _cursorPosition;
    private Rigidbody _characterRb;
    private Vector3 _look;
    private bool _canShoot = false;

    private void Start()
    {
        _attakReloadTimer = new Timer(AttakReload);
        _characterRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_canShoot)
        {
            Fire();
        }
    }

    private void FixedUpdate()
    {
        Reload();
        Rotate();

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

    private void Rotate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetDirection = transform.position - mousePosition;

        Quaternion angle = Quaternion.LookRotation(Vector3.forward, targetDirection);
        _characterRb.rotation = angle;
    }
}
