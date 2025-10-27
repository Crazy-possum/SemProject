using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletBehavior : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _damage = 200;

    [SerializeField] private float _firstPaintingStage = 0.25f;
    [SerializeField] private float _secondPaintingStage = 0.5f;
    [SerializeField] private float _thirdPaintingStage = 0.75f;
    [SerializeField] private float _fourthPaintingStage = 1f;

    public GameObject BulletsCurrentTarget;

    private EnemyParametrs _currentEnemyHealth;

    private Rigidbody _bulletRB;
    private Vector3 _movement;

    private void Start()
    {
        _currentEnemyHealth = BulletsCurrentTarget.GetComponent<EnemyParametrs>();
        _bulletRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void FixedUpdate()
    {
        _bulletRB.velocity = _movement;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            switch (enemy.GetComponent<EnemyParametrs>().CurrentPaint)
            {
                case 1: _currentEnemyHealth.CurrentHealth = _currentEnemyHealth.CurrentHealth - _damage * _firstPaintingStage; break;
                case 2: _currentEnemyHealth.CurrentHealth = _currentEnemyHealth.CurrentHealth - _damage * _secondPaintingStage; break;
                case 3: _currentEnemyHealth.CurrentHealth = _currentEnemyHealth.CurrentHealth - _damage * _thirdPaintingStage; break;
                case 4: _currentEnemyHealth.CurrentHealth = _currentEnemyHealth.CurrentHealth - _damage * _fourthPaintingStage; break;
                default: break;
            }

            Destroy(gameObject);
        }
    }

    private void MoveToTarget()
    {
        float deltaX = BulletsCurrentTarget.transform.position.x - gameObject.transform.position.x;
        float deltaY = BulletsCurrentTarget.transform.position.y - gameObject.transform.position.y;

        Vector3 move = new Vector3(deltaX, deltaY, 0).normalized;

        _movement.Set(_speed * move.x, _speed * move.y, _speed * move.z);
    }
}
