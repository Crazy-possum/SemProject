using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _damage = 2;

    public GameObject BulletsCurrentTarget;

    private EconomyController _economyController;
    private EnemyHealth _currentEnemyHealth;

    private Rigidbody _bulletRB;
    private Vector3 _movement;

    public EconomyController EconomyController { get => _economyController; set => _economyController = value; }

    private void Start()
    {
        _currentEnemyHealth = BulletsCurrentTarget.GetComponent<EnemyHealth>();
        _bulletRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float deltaX = BulletsCurrentTarget.transform.position.x - gameObject.transform.position.x;
        float deltaZ = BulletsCurrentTarget.transform.position.z - gameObject.transform.position.z;

        Vector3 move = new Vector3(deltaX, 0, deltaZ);
        move.Normalize();

        _movement.Set(_speed * move.x, _speed * move.y, _speed * move.z);
    }

    private void FixedUpdate()
    {
        _bulletRB.velocity = _movement;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyPatrol enemy))
        {
            Debug.Log("damage" + _damage);
            _currentEnemyHealth.CurrentHealth = _currentEnemyHealth.CurrentHealth - _damage;
            Debug.Log(_currentEnemyHealth.CurrentHealth);

            _economyController.CurrentIncome = 1;
            _economyController.GetCurrency();

            Destroy(gameObject);
        }
    }
}
