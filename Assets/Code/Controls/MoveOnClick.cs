using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveOnClick : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _stoppingDistance = 0.5f;
    [SerializeField] private float _rotationSpeed = 5f;

    private NavMeshAgent _character;
    private Animator _animator;
    private Vector3 _targetPosition;
    private bool _hasTarget = false;
    private Coroutine _movementCoroutine;

    private void Start()
    {
        _character = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        if (_character != null)
        {
            _character.speed = _speed;
            _character.stoppingDistance = _stoppingDistance;
            _character.angularSpeed = 360f;
            _character.acceleration = 8f;
            _character.autoBraking = true;
        }
    }

    public void ClickChecker(Vector3 mouseTransform)
    {
        if (Input.GetMouseButtonDown(0))
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(mouseTransform, out hit, 1.0f, NavMesh.AllAreas))
            {
                SetTargetPosition(hit.position);
            }
        }
    }

    private void SetTargetPosition(Vector3 position)
    {
        if (_character != null && _character.isOnNavMesh)
        {
            _targetPosition = position;
            _hasTarget = true;

            if (_character.SetDestination(_targetPosition))
            {
                if (_movementCoroutine != null)
                {
                    StopCoroutine(_movementCoroutine);
                }
                _movementCoroutine = StartCoroutine(CheckMovementProgress());
            }
            else
            {
                _hasTarget = false;
            }
        }
    }

    private IEnumerator CheckMovementProgress()
    {
        while (_hasTarget && _character != null && _character.pathPending)
        {
            yield return null;
        }

        if (_character != null && _character.hasPath)
        {
            _animator.SetBool("isWalk", true);
            while (_hasTarget && _character.remainingDistance > _stoppingDistance)
            {
                if (_character.velocity.sqrMagnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(_character.velocity.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                        _rotationSpeed * Time.deltaTime);
                }
                yield return null;
            }

            if (_hasTarget)
            {
                StopMovement();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void StopMovement()
    {
        _hasTarget = false;
        _animator.SetBool("isWalk", false);

        if (_character != null)
        {
            _character.isStopped = true;
            _character.ResetPath();
        }

        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
            _movementCoroutine = null;
        }
    }

    public bool IsMoving()
    {
        return _hasTarget && _character != null && _character.remainingDistance > _stoppingDistance;
    }

    public Vector3 GetTargetPosition()
    {
        return _hasTarget ? _targetPosition : transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        if (_hasTarget && _character != null && _character.hasPath)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_targetPosition, 0.2f);
            Gizmos.DrawWireSphere(_targetPosition, _stoppingDistance);

            Gizmos.color = Color.green;
            Vector3[] corners = _character.path.corners;
            for (int i = 0; i < corners.Length - 1; i++)
            {
                Gizmos.DrawLine(corners[i], corners[i + 1]);
                Gizmos.DrawSphere(corners[i], 0.1f);
            }
            if (corners.Length > 0)
            {
                Gizmos.DrawSphere(corners[corners.Length - 1], 0.1f);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _character.velocity.normalized * 2f);
        }
    }

    public void MoveToObject(GameObject targetObject, float customStoppingDistance = -1)
    {
        if (targetObject != null && _character != null && _character.isOnNavMesh)
        {
            if (customStoppingDistance >= 0)
            {
                _character.stoppingDistance = customStoppingDistance;
            }

            SetTargetPosition(targetObject.transform.position);
        }
    }
}
