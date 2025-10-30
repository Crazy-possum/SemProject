using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    [Tooltip("������ �������")]
    [SerializeField] private GameObject _bullet;
    [Tooltip("����� ������ ��������")]
    [SerializeField] private Transform _bulletSpawner;

    public float CurrentTime;

    private Timer _attakReloadTimer;
    private Rigidbody _characterRb;
    private Camera _camera;
    private float _attakReload = 2;
    private bool _isCanShoot = false;

    public float AttakReload { get => _attakReload; set => _attakReload = value; }

    private void Start()
    {
        _attakReloadTimer = new Timer(_attakReload);
        _characterRb = GetComponent<Rigidbody>();
        _camera = Camera.main;
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
        Rotate();

        CurrentTime = _attakReloadTimer.TimerCurrentTime;
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

    private void Fire()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0)))
        {
            Vector3 position = _bulletSpawner.position;
            GameObject localBullet = GameObject.Instantiate(_bullet, position, Quaternion.identity, _bulletSpawner);
            localBullet.GetComponent<CharacterBulletBehavior>().StartBulletPosition = _bulletSpawner;

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
}
