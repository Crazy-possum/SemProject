using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBulletBehavior : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _painting = 1;
    [SerializeField] private int _maxDistance;

    public Transform StartBulletPosition;

    private Vector3 _movement;

    private void Update()
    {
        MoveOnVector();
        CheckDistance();
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().velocity = _movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyParametrs enemy))
        {
            if (enemy.GetComponent<EnemyParametrs>().CurrentPaint <= 4)
            {
                enemy.GetComponent<EnemyParametrs>().CurrentPaint = enemy.GetComponent<EnemyParametrs>().CurrentPaint + _painting;
                Destroy(gameObject);
            }
        }
    }

    private void MoveOnVector()
    {
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - StartBulletPosition.position).normalized;
        _movement.Set(direction.x * _speed, direction.y * _speed, direction.z * 0);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(StartBulletPosition.position, gameObject.transform.position);
        if (distance >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
