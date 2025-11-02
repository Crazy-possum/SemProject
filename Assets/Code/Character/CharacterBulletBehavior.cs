using UnityEngine;

public class CharacterBulletBehavior : MonoBehaviour
{
    [Tooltip("Скорость полета снаряда")]
    [SerializeField] private float _speed = 100f;
    [Tooltip("Значение покраса от 1 попадания снаряда")]
    [SerializeField] private float _painting = 1;
    [Tooltip("Максимальная дистанция полета снаряда")]
    [SerializeField] private int _maxDistance;

    private Transform _startBulletPosition;
    private Rigidbody _bulletRb;
    private Camera _camera;
    private Vector3 _movement;

    public Transform StartBulletPosition { get => _startBulletPosition; set => _startBulletPosition = value; }

    private void Start()
    {
        _bulletRb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        _bulletRb.velocity = _movement;
        MoveOnVector();
        CheckDistance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            EnemyParametrs enemyParametrs = enemy.GetComponent<EnemyParametrs>();

            if (enemyParametrs.CurrentPaintValue < 4)
            {
                enemyParametrs.CurrentPaintValue += _painting;
                Destroy(gameObject);
            }
        }
    }

    private void MoveOnVector()
    {
        Vector3 direction = (_camera.ScreenToWorldPoint(Input.mousePosition) - _startBulletPosition.position).normalized;
        _movement.Set(direction.x * _speed, direction.y * _speed, 0);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(_startBulletPosition.position, gameObject.transform.position);
        if (distance >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
