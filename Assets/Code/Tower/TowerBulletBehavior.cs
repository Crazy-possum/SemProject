using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletBehavior : MonoBehaviour
{
    [Tooltip("Скорость полета снаряда")]
    [SerializeField] private float _speed = 100f;
    [Tooltip("Урон от снаряда")]
    [SerializeField] private float _damage = 200;

    [Tooltip("Процент пробития моба при 1 единичке окрашивания")]
    [SerializeField] private float _firstPaintingStage = 0.25f;
    [Tooltip("Процент пробития моба при 2 единичках окрашивания")]
    [SerializeField] private float _secondPaintingStage = 0.5f;
    [Tooltip("Процент пробития моба при 3 единичках окрашивания")]
    [SerializeField] private float _thirdPaintingStage = 0.75f;
    [Tooltip("Процент пробития моба при 4 единичках окрашивания")]
    [SerializeField] private float _fourthPaintingStage = 1f;

    private GameObject _bulletsCurrentTarget;
    private EnemyParametrs _currentEnemyHealth;

    private Rigidbody _bulletRB;
    private Vector3 _movement;

    public GameObject BulletsCurrentTarget { get => _bulletsCurrentTarget; set => _bulletsCurrentTarget = value; }

    private void Start()
    {
        _currentEnemyHealth = _bulletsCurrentTarget.GetComponent<EnemyParametrs>();
        _bulletRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
        _bulletRB.velocity = _movement;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            float colorValue = enemy.GetComponent<EnemyParametrs>().CurrentPaintValue;
            float currentHealth = _currentEnemyHealth.CurrentHealth;

            switch (colorValue)
            {
                case 1: currentHealth = currentHealth - (_damage * _firstPaintingStage); break;
                case 2: currentHealth = currentHealth - (_damage * _secondPaintingStage); break;
                case 3: currentHealth = currentHealth - (_damage * _thirdPaintingStage); break;
                case 4: currentHealth = currentHealth - (_damage * _fourthPaintingStage); break;
            }

            Destroy(gameObject);
        }
    }

    private void MoveToTarget()
    {
        float deltaX = _bulletsCurrentTarget.transform.position.x - gameObject.transform.position.x;
        float deltaY = _bulletsCurrentTarget.transform.position.y - gameObject.transform.position.y;

        Vector3 move = new Vector3(deltaX, deltaY, 0).normalized;

        _movement.Set(_speed * move.x, _speed * move.y, _speed * move.z);
    }
}
