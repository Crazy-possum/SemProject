using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShoot : MonoBehaviour
{
    [Tooltip("Префаб снаряда")]
    [SerializeField] private GameObject _bullet;
    [Tooltip("Точка вылета снарядов")]
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private Transform _doublebulletSpawnerLeft;
    [SerializeField] private Transform _doublebulletSpawnerRight;

    public float CurrentTime;

    private Timer _attakReloadTimer;
    private Rigidbody _characterRb;
    private Slider _reloadSlider;
    private Camera _camera;
    private float _attakReload = 2;
    private float _currentReloadTime;
    private bool _isCanShoot = false;
    private bool _isDoubleShotOn;
    private float _currentTimerTime;
    private float _reloadTime;

    public float AttakReload { get => _attakReload; set => _attakReload = value; }
    public Slider ReloadSlider { get => _reloadSlider; set => _reloadSlider = value; }

    private void Start()
    {
        _attakReloadTimer = new Timer(_attakReload);
        _currentReloadTime = _attakReload;
        _characterRb = GetComponent<Rigidbody>();
        _camera = Camera.main;

        TimerSliderUpdate();
    }

    private void Update()
    {
        if (_isCanShoot)
        {
            Fire();
        }
    }

    private void FixedUpdate()
    {
        Reload();
        SetTimerSlider();
        TimerSliderUpdate();
        Rotate();

        CurrentTime = _attakReloadTimer.TimerCurrentTime;
    }

    private void OnEnable()
    {
        CharacterUpgrader.OnSpeedUpCharReload += CutReloadTime;
        CharacterUpgrader.OnDoubleShot += ActivateDoubleShot;
    }

    private void OnDisable()
    {
        CharacterUpgrader.OnSpeedUpCharReload -= CutReloadTime;
        CharacterUpgrader.OnDoubleShot -= ActivateDoubleShot;
    }

    private void SetTimerSlider()
    {
        _reloadTime = AttakReload;
        _reloadSlider.maxValue = _reloadTime;
    }

    private void Reload()
    {
         _attakReloadTimer.Wait();

        if (!_attakReloadTimer.StartTimer && !_isCanShoot)
        {
            _attakReloadTimer.StartCountdown();
        }

        if (_attakReloadTimer.ReachingTimerMaxValue == true)
        {
            _isCanShoot = true;
            _attakReloadTimer.PauseCountdown();
        }
    }

    private void TimerSliderUpdate()
    {
        _currentTimerTime = CurrentTime;
        _reloadSlider.value = _currentTimerTime;
    }

    private void Fire()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if (!_isDoubleShotOn)
            {
                Vector3 position = _bulletSpawner.position;
                GameObject localBullet = GameObject.Instantiate(_bullet, position, Quaternion.identity, _bulletSpawner);
                localBullet.GetComponent<CharacterBulletBehavior>().StartBulletPosition = _bulletSpawner;
            }
            else
            {
                Vector3 leftPosition = _doublebulletSpawnerLeft.position;
                GameObject localLeftBullet = GameObject.Instantiate(_bullet, leftPosition, Quaternion.identity, _bulletSpawner);
                localLeftBullet.GetComponent<CharacterBulletBehavior>().StartBulletPosition = _doublebulletSpawnerLeft;

                Vector3 rightPosition = _doublebulletSpawnerRight.position;
                GameObject localRightBullet = GameObject.Instantiate(_bullet, rightPosition, Quaternion.identity, _bulletSpawner);
                localRightBullet.GetComponent<CharacterBulletBehavior>().StartBulletPosition = _doublebulletSpawnerRight;
            }

            _attakReloadTimer.StopCountdown();
            _isCanShoot = false;
        }
    }

    private void Rotate()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetDirection = transform.position - mousePosition;

        Quaternion angle = Quaternion.LookRotation(Vector3.forward, targetDirection);
        _characterRb.rotation = angle;
    }

    private void CutReloadTime(float cutCharReload)
    {
        _attakReload = _attakReload - (_attakReload * cutCharReload);
        _attakReloadTimer.ResetTimerMaxTime(_attakReload);
        SetTimerSlider();
    }

    private void ActivateDoubleShot()
    {
        _isDoubleShotOn = true;
    }
}
