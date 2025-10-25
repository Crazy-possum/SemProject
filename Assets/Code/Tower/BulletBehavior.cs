using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _damage = 2;

    public GameObject BulletsCurrentTarget;

    private EnemyHealth _currentEnemyHealth;

    private Rigidbody _bulletRB;
    private Vector3 _movement;

    private void Start()
    {
        _currentEnemyHealth = BulletsCurrentTarget.GetComponent<EnemyHealth>();
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
        if (other.gameObject.TryGetComponent(out EnemyMovement enemy))
        {
            _currentEnemyHealth.CurrentHealth = _currentEnemyHealth.CurrentHealth - _damage;

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
